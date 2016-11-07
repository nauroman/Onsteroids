using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System;

namespace Flashunity.Drones
{
    [Serializable]
    public class ImpulseGun : Weapon
    {
        public Transform impulse;

        public Dictionary<byte, BImpulse> bfires;
        byte fireId = 1;

        public void Fire(Vector3 position, Quaternion rotation, byte fireId, short fromPhotonViewId)
        {
            cooldownTimer = cooldownTime;
            charge--;

            var impulse = GameObject.Instantiate(this.impulse, position, rotation);

            var bimpulse = impulse.GetComponent<BImpulse>();
            bimpulse.fromPhotonViewId = fromPhotonViewId;
            bimpulse.fireId = fireId;

            var rb = impulse.GetComponent<Rigidbody>();

            rb.AddRelativeForce(new Vector3(0, 0, 40), ForceMode.VelocityChange);

            if (bfires == null)
                bfires = new Dictionary<byte, BImpulse>();
            
            bfires [fireId] = bimpulse;
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
            photonView.RPC("FireImpulse", PhotonTargets.All, new object[]
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