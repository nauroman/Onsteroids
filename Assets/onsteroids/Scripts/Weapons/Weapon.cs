using UnityEngine;
using System.Runtime.InteropServices;

namespace Flashunity.Drones
{
    public abstract class Weapon
    {
        public short damage;

        public int maxCharge = 0;
        public int charge = 0;

        public float maxDistance = 1000;

        public float cooldownTime = 0.1f;

        [HideInInspector]
        public float cooldownTimer = 0;

        public abstract void RPC(Vector3 at, PhotonView photonView);

        public void AddCooldownTime(float time)
        {
            cooldownTimer += time;
        }

        public abstract bool IsReadyToFire{ get; }

        public void Update()
        {
            if (cooldownTimer > 0)
                cooldownTimer -= Time.deltaTime;
            
            if (cooldownTimer < 0)
                cooldownTimer = 0;
        }
    }
}