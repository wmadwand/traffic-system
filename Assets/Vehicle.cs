using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Vehicle : MonoBehaviour
{
    public float speed;
    [SerializeField] private float _fieldView = .95f;

    public float rotationRate;

    private Transform _player;
    private Vector3 vectorDir;
    private float vectorDot;

    bool isStopedOnTrafficLight;
    int currentTrafficLightID;

    Rigidbody m_Rigidbody;
    Vector3 m_EulerAngleVelocity;

    Coroutine cor;

    private void Awake()
    {
        TrafficLight.OnLightChange += TrafficLight_OnLightChange;

        m_EulerAngleVelocity = new Vector3(0, 100, 0);

        m_Rigidbody = GetComponent<Rigidbody>();
    }

    private void TrafficLight_OnLightChange(int arg1, LightColor arg2)
    {
        if (currentTrafficLightID == arg1 && arg2 == LightColor.green)
        {
            isStopedOnTrafficLight = false;
        }
    }

    bool isTurning;

    private void FixedUpdate()
    {
        if (isStopedOnTrafficLight)
        {
            GetComponent<Rigidbody>().velocity = Vector3.Lerp(GetComponent<Rigidbody>().velocity, Vector3.zero, 10 * Time.fixedDeltaTime);
            return;
        }

        GetComponent<Rigidbody>().velocity = transform.forward * speed * Time.fixedDeltaTime;

    }

    IEnumerator TurnObj(Vector3 vecAngle)
    {
        var quatTarget = Quaternion.Euler(vecAngle);

        while (Quaternion.Angle(m_Rigidbody.rotation, quatTarget) > 0.1f /*0.01f*/)
        {
            TurnObject(vecAngle);

            yield return new WaitForFixedUpdate();

        }

        cor = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<TrafficLight>())
        {
            if (IsFaceToObject(other.transform.forward))
            {
                if (!other.GetComponent<TrafficLight>().IsGreenLight())
                {
                    isStopedOnTrafficLight = true;
                    currentTrafficLightID = other.GetComponent<TrafficLight>().id;
                }
                else
                {
                    isStopedOnTrafficLight = false;
                }
            }
        }

        if (other.GetComponent<Router>() && !isStopedOnTrafficLight && cor == null && !gotTurn)
        {
            var directions = other.GetComponent<Router>().possibleDirections;

            var randdomIndex = Random.Range(0, directions.Length);

            gotTurn = true;

            Turn(directions[randdomIndex]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Router>())
        {
            //StopCoroutine(cor);
            
            gotTurn = false;

        }
    }

    public bool IsFaceToObject(Vector3 target)
    {
        //vectorDir = target - transform.position;
        vectorDot = Vector3.Dot(transform.forward.normalized, target.normalized);

        return vectorDot < _fieldView;
    }

    bool gotTurn;

    void TurnObject(Vector3 vecAngle)
    {
        Quaternion deltaRotation = Quaternion.Euler(vecAngle * rotationRate * Time.deltaTime);
        m_Rigidbody.MoveRotation(m_Rigidbody.rotation * deltaRotation);
    }

    void Turn(DraggedDirection dir)
    {
        switch (dir)
        {
            case DraggedDirection.Up:
                cor = StartCoroutine(TurnObj(new Vector3(0, 0, 0)));
                break;
            case DraggedDirection.Down:
                break;
            case DraggedDirection.Right:
                cor = StartCoroutine(TurnObj(new Vector3(0, 90, 0)));

                break;
            case DraggedDirection.Left:
                cor = StartCoroutine(TurnObj(new Vector3(0, -90, 0)));
                break;
            default:
                break;
        }
    }
}
