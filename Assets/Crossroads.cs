using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Crossroads : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public event Action<float, bool, bool> OnInteract;

    public TrafficLight[] trafficLights;

    private Vector2 _origin;
    private Vector2 _direction;

    private bool _touched;
    private int _pointerID;

    private void Awake()
    {
        _touched = false;
        _direction = Vector2.zero;
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData data)
    {
        if (!_touched)
        {
            _touched = true;
            _pointerID = data.pointerId;
            _origin = data.position;

            fingerDown = data.position;
        }
    }

    private Vector2 fingerDown;
    private Vector2 fingerUp;

    void IDragHandler.OnDrag(PointerEventData data)
    {
        if (data.pointerId == _pointerID)
        {
            Vector2 currentPosition = data.position;
            _direction = currentPosition - _origin;
        }
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData data)
    {
        fingerUp = data.position;

        var horMove = Mathf.Abs(fingerDown.x - fingerUp.x);
        var vertMove = Mathf.Abs(fingerDown.y - fingerUp.y);

        // horizontal
        if (horMove > vertMove)
        {

        }
        else // vertical
        {
            if (fingerDown.y - fingerUp.y < 0)
            {
                Debug.Log("Swipe up");
            }
            else if (fingerDown.y - fingerUp.y > 0)
            {
                Debug.Log("Swipe down");
            }
        }

        //Debug.Log($"dir: {_direction}");
        Debug.Log($"dir norm: {_direction.normalized}");


        if (Mathf.Abs(_direction.normalized.y) < Mathf.Abs(_direction.normalized.x))
        {
            var normalizedDirX = _direction.normalized.x < 0 ? -1 : 1;

            Debug.Log($"Horizontal: {normalizedDirX}");
        }
        else
        {
            var normalizedDirY = _direction.normalized.y < 0 ? -1 : 1;

            Debug.Log($"Verical: {normalizedDirY}");
        }



        CallTrafficLight(_direction.normalized);

    }

    public float dotVal = 0.6f;

    void CallTrafficLight(Vector2 dirNOrm)
    {
        var tl = trafficLights[0];


        var tlNormal = /*(Vector2) */tl.gameObject.transform.forward.normalized;

        var resVeccc = new Vector2(tlNormal.x, tlNormal.z);

        Debug.Log($"resVeccc:{resVeccc}");        
        Debug.Log($"dirNOrm:{dirNOrm}");

        var ccc = resVeccc - dirNOrm;

        //var vecRes = resVeccc - dirNOrm;

        //Debug.Log($"vecRes:{vecRes}");

        var dot = Vector3.Dot(ccc, dirNOrm);

        Debug.Log($"dot:{dot}");

        if (dot > dotVal)
        {
            tl.ChangeColor();
        }
    }
}
