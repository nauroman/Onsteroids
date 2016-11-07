using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flashunity.Drones
{
    public class BLoop : MonoBehaviour
    {
        BCamera bcamera;

        [SerializeField]
        Rigidbody rb;

        [SerializeField]
        SphereCollider sphereCollider;

        void FixedUpdate()
        {
            LoopPosition();
        }

        void LoopPosition()
        {
            if (bcamera == null)
            {
                var mainCamera = GameObject.Find("Main Camera");
                if (mainCamera)
                {
                    bcamera = mainCamera.GetComponent<BCamera>();
                }
            }

            if (sphereCollider && rb && bcamera && bcamera.FrustumWidth > 0 && bcamera.FrustumHeight > 0)
            {
                Vector3 newPosition;

                if (GetNewPosition(rb.position, sphereCollider.radius, bcamera.FrustumWidth, bcamera.FrustumHeight, out newPosition))
                {
                    rb.MovePosition(newPosition);
                }
            }
        }

        bool GetNewPosition(Vector3 position, float radius, float frustumWidth, float frustumHeight, out Vector3 newPosition)
        {
            newPosition = position;

            bool loop = false;

            if (position.x + radius < -frustumWidth)
            {
                newPosition.x = frustumWidth + radius;
                loop = true;
            } else if (position.x - radius > frustumWidth)
            {
                newPosition.x = -frustumWidth - radius;
                loop = true;
            }

            if (position.z + radius < -frustumHeight)
            {
                newPosition.z = frustumHeight + radius;
                loop = true;
            } else if (position.z - radius > frustumHeight)
            {
                newPosition.z = -frustumHeight - radius;
                loop = true;
            }


            return loop;
        }
    }
}