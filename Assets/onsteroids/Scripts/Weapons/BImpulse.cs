using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;


namespace Flashunity.Drones
{
    public class BImpulse : MonoBehaviour
    {
        [HideInInspector]
        public short fromPhotonViewId;

        [HideInInspector]
        public byte fireId = 0;

        public float lifeTime = 3;

        void OnCollisionEnter(Collision collision)
        {
            var photonView = collision.transform.root.GetComponent<PhotonView>();

            if (PhotonNetwork.inRoom && photonView != null)
            {
                if (photonView.viewID != fromPhotonViewId)
                {
                    byte armorPart = 0;

                    photonView.RPC("HitImpulse", PhotonTargets.All, new object[]
                    {                
                        collision.contacts [0].point, armorPart, fromPhotonViewId, fireId
                    });
                }
            } else
            {
                var bdestructable = collision.transform.root.GetComponent<BDestructable>();

                if (bdestructable != null)
                {
                    if (bdestructable.HitImpulse())
                    {
                        Destroy(gameObject);
                    }
                } 
            }


            return;

            //          Debug.Log("collision.gameObject.name: " + collision.collider.name);

//            StartCoroutine(FadeOutThreeD(collision.collider.transform, 0, false, 10));

            /*
            return;

            var fade = collision.collider.GetComponent<FadeObjectInOut>();

            Debug.Log("fade: " + fade);

            if (fade != null)
            {
                fade.FadeIn();
            }

            return;
*/
//            Debug.Log("collision.transform.root.name: " + collision.transform.root.name);
            //     Debug.Log("collision.transform.root.name: " + collision.gameObject.name);

            //   return;



/*
            //     return;

            var bdrone = collision.transform.root.GetComponent<BDrone>();

//            var bdrone = other.GetComponentInParent<BDrone>();

            if (bdrone != null)
            {
                Debug.Log("OnCollisionEnter.bdrone");
            }
*/
//            var bdrone = collision.collider.GetComponentInParent<BDrone>();

//            var bdrone = collision.transform.root.GetComponent<BDrone>();



            /*
            //     Debug.Log("OnCollisionEnter");

            rb = GetComponent<Rigidbody>();
            onCollisionEnterVelocity = rb.velocity;

            //       Debug.Log("onCollisionEnterVelocity:" + onCollisionEnterVelocity);

            float newLifeTime = 0;
            if (UnityEngine.Random.value > 0.5f)
                newLifeTime = UnityEngine.Random.value / 3f;

            newLifeTime = 1;

            lifeTime = Math.Min(lifeTime, newLifeTime);

            if (lifeTime > 0)
            {
                var contactPoint = collision.contacts [0];

                var magnitude = rb.velocity.magnitude;
                var randMagnitude = magnitude / 10;

                var newDirection = Vector3.Reflect(rb.velocity, contactPoint.normal);

                newDirection -= rb.velocity;

                newDirection.x += UnityEngine.Random.Range(-randMagnitude, randMagnitude);
                newDirection.y += UnityEngine.Random.Range(-randMagnitude, randMagnitude);
                newDirection.z += UnityEngine.Random.Range(-randMagnitude, randMagnitude);

                var dot = Vector3.Dot(newDirection.normalized, contactPoint.normal);

                //    Debug.Log("dot:  " + dot);

//                if (contactPoint.normal)

//                rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                rb.AddForce(newDirection, ForceMode.VelocityChange);
//                rb.AddRelativeForce(Vector3, ForceMode.VelocityChange);


            } else
            {
                Destroy(gameObject);
            }
            */

//            if (collision.relativeVelocity.magnitude > 2)
            //              audio.Play();

        }

        void OnCollisionExit(Collision collision)
        {
            /*
            if (rb != null)
            {
                var velocity = rb.velocity;
                Debug.Log("velocity:" + velocity);

                var dot = Vector3.Dot(velocity.normalized, onCollisionEnterVelocity.normalized);

                Debug.Log("dot:" + dot);

                Destroy(gameObject);
//                velocity 

//                if (Vector3.Dot(velocity.normalized, onCollisionEnterVelocity.normalized) > 0.1f)
            } else
                Destroy(gameObject);
            
//            Debug.Log("OnCollisionExit");
*/
        }

        void Start()
        {
		
        }

        void Update()
        {
            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0)
                Destroy(gameObject);
        }
    }

}


// A grenade
// - instantiates a explosion prefab when hitting a surface
// - then destroys itself
/*
using UnityEngine;
using System.Collections;

public class ExampleClass : MonoBehaviour {

    public Transform explosionPrefab;

    void OnCollisionEnter(Collision collision) {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(explosionPrefab, pos, rot);
        Destroy(gameObject);
    }
}
*/