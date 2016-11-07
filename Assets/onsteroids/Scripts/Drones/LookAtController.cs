using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flashunity.Drones
{

    public class LookAtController : MonoBehaviour
    {
        private readonly VectorPid angularVelocityController = new VectorPid(50, 0, 0.06f);
        private readonly VectorPid headingController = new VectorPid(25, 0, 0.06382979f);

        public Transform target;

        Rigidbody rb;

        void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            var angularVelocityError = rb.angularVelocity * -1;
//            Debug.DrawRay(transform.position, rb.angularVelocity * 10, Color.black);

            var angularVelocityCorrection = angularVelocityController.Update(angularVelocityError, Time.deltaTime);
            //          Debug.DrawRay(transform.position, angularVelocityCorrection, Color.green);

            rb.AddTorque(angularVelocityCorrection);



            var desiredHeading = target.position - transform.position;
            //        Debug.DrawRay(transform.position, desiredHeading, Color.magenta);

            var currentHeading = transform.up;
            //      Debug.DrawRay(transform.position, currentHeading * 15, Color.blue);

            var headingError = Vector3.Cross(currentHeading, desiredHeading);
            var headingCorrection = headingController.Update(headingError, Time.deltaTime);

            rb.AddTorque(headingCorrection);

        }
    }
}
