using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flashunity.Drones
{
    public class BRocket : MonoBehaviour
    {
        [HideInInspector]
        public short fromPhotonViewId;

        [HideInInspector]
        public byte fireId;

        public float lifeTime = 6;

        void OnTriggerEnter(Collider other)
        {
            /*
            if (other.material != null)
            {
                var materialName = other.material.name;

                if (materialName.StartsWith("metall"))
                {
                
                }

//                Destroy(other.gameObject);
            }

            Destroy(gameObject);
            */
        }

        void OnCollisionEnter(Collision collision)
        {
            var photonView = collision.transform.root.GetComponent<PhotonView>();

            if (photonView != null && photonView.viewID != fromPhotonViewId)
            {
                byte armorPart = 0;

                byte damage = 100;

                photonView.RPC("HitRocket", PhotonTargets.All, new object[]
                {                
                    collision.contacts [0].point, armorPart, fromPhotonViewId, fireId, damage
                });
            }
        }

        void Update()
        {
            lifeTime -= Time.deltaTime;

            if (lifeTime <= 0)
                Destroy(gameObject);
        }
    }

}