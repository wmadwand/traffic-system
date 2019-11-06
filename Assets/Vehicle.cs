using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Vehicle : MonoBehaviour
{
    public float speed;

    private void Start()
    {
        //GetComponent<Rigidbody>().velocity = transform.forward * 5;
    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody>().velocity = transform.forward * speed * Time.fixedDeltaTime;
    }
}
