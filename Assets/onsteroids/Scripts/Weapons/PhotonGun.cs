using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System;

namespace Flashunity.Drones
{
    [Serializable]
    public class PhotonGun : Weapon
    {
        public Transform photon;

        public void Fire(Vector3 position, Quaternion rotation)
        {
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
            photonView.RPC("FirePhoton", PhotonTargets.All, new object[]
            {
                at
            });
        }

    }
}