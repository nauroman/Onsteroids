using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BAddRandomTorque : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;

    [SerializeField]
    float torque;

    void Start()
    {        
        rb.AddTorque(UnityEngine.Random.Range(-torque, torque), UnityEngine.Random.Range(-torque, torque), UnityEngine.Random.Range(-torque, torque), ForceMode.VelocityChange);
    }
}
