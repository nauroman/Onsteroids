using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System;

namespace Flashunity.Drones
{
    [Serializable]
    public class RocketGun : Weapon
    {
        public Transform rocket;

        public Dictionary<byte, BRocket> bfires;
        byte fireId = 1;

        public void Fire(Vector3 position, Quaternion rotation, byte fireId, short fromPhotonViewId)
        {
            cooldownTimer = cooldownTime;
            charge--;

            var rocket = GameObject.Instantiate(this.rocket, position, rotation);

            var brocket = rocket.GetComponent<BRocket>();
            brocket.fromPhotonViewId = fromPhotonViewId;
            brocket.fireId = fireId;

            var rb = rocket.GetComponent<Rigidbody>();

            rb.AddRelativeForce(new Vector3(0, 0, 20), ForceMode.VelocityChange);

            if (bfires == null)
                bfires = new Dictionary<byte, BRocket>();

            bfires [fireId] = brocket;
        }

        public override bool IsReadyToFire
        {
            get
            {
                return cooldownTimer <= 0 && charge > 0;
            }
        }

        public override void RPC(Vector3 at, PhotonView photonView)
        {
            photonView.RPC("FireRocket", PhotonTargets.All, new object[]
            {
                at, fireId
            });

            if (fireId == 255)
                fireId = 1;
            else
                fireId++;
        }

        public void DestroyFire(byte fireId)
        {
            if (bfires != null)
            {                
                var bfire = bfires [fireId];

                if (bfire != null)
                {
                    GameObject.Destroy(bfire.gameObject);
                }
            }
        }

    }
}