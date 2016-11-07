using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAddRandomForce : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    float velocity;

    void Start()
    {        
        rb.AddForce(UnityEngine.Random.Range(-velocity, velocity), UnityEngine.Random.Range(-velocity, velocity), UnityEngine.Random.Range(-velocity, velocity), ForceMode.VelocityChange);
    }
}
