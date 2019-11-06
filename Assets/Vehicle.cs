﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Vehicle : MonoBehaviour
{
    public float speed;
    [SerializeField] private float _fieldView = .95f;

    private Transform _player;
    private Vector3 vectorDir;
    private float vectorDot;

    bool isStopedOnTrafficLight;
    int currentTrafficLightID;

    private void Awake()
    {
        TrafficLight.OnLightChange += TrafficLight_OnLightChange;
    }

    private void TrafficLight_OnLightChange(int arg1, LightColor arg2)
    {
        if (currentTrafficLightID == arg1 && arg2==LightColor.green)
        {
            isStopedOnTrafficLight = false;
        }
    }

    private void Start()
    {
        //GetComponent<Rigidbody>().velocity = transform.forward * 5;
    }

    private void FixedUpdate()
    {
        if (isStopedOnTrafficLight)
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }
        
        GetComponent<Rigidbody>().velocity = transform.forward * speed * Time.fixedDeltaTime;
    }

    private void Update()
    {

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
    }

    public bool IsFaceToObject(Vector3 target)
    {
        //vectorDir = target - transform.position;
        vectorDot = Vector3.Dot(transform.forward.normalized, target.normalized);

        return vectorDot < _fieldView;
    }
}
