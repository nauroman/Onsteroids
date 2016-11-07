using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Flashunity.Drones;

namespace Flashunity.Drones
{
    [Serializable]
    public class Weapons
    {
        private BProgressBar bammoProgressBar;

        public Collider bodyCollider;
        public BDrone bdrone;

        public float switchTime = 0.2f;

        [HideInInspector]
        public float switchTimer = 0;

        public Transform gunFire;

        [SerializeField]
        public BeamGun beamGun;
        [SerializeField]
        public ImpulseGun impulseGun;
        [SerializeField]
        public RocketGun rocketGun;
        [SerializeField]
        public StreamGun streamGun;
        [SerializeField]
        public PhotonGun photonGun;

        [SerializeField]
        PhotonView photonView;

        //        private byte fireId = 1;

        public Weapon weapon;

        [HideInInspector]
        Weapon[] weapons;

        public bool ButtonFire()
        {
            if (weapon != null && weapon.IsReadyToFire)
            {
                var at = gunFire.forward * weapon.maxDistance;

                if (bdrone.offline)
                {
                    FireImpulse(at, 0);
                } else
                {
                    weapon.RPC(at, photonView);
                }

                return true;
            }
            return false;
        }

        public void FireBeam(Vector3 at)
        {
            if (weapon == beamGun)
                weapon = beamGun;

            gunFire.LookAt(at);
            beamGun.Fire(gunFire.position, gunFire.rotation);
        }

        public void FireImpulse(Vector3 at, byte fireId)
        {
            if (weapon == impulseGun)
                weapon = impulseGun;
            
            //      gunFire.LookAt(at);
            impulseGun.Fire(gunFire.position, gunFire.rotation, fireId, (short)photonView.viewID);
        }

        public void FireRocket(Vector3 at, byte fireId)
        {
            if (weapon == rocketGun)
                weapon = rocketGun;
            
            gunFire.LookAt(at);
            rocketGun.Fire(gunFire.position, gunFire.rotation, fireId, (short)photonView.viewID);
        }

        public void FireStream(Vector3 at)
        {
            if (weapon == streamGun)
                weapon = streamGun;
            
            gunFire.LookAt(at);
            streamGun.Fire(gunFire.position, gunFire.rotation);
        }

        public void FirePhoton(Vector3 at)
        {
            if (weapon == photonGun)
                weapon = photonGun;
            
            gunFire.LookAt(at);
            photonGun.Fire(gunFire.position, gunFire.rotation);
        }



        public void Update()
        {
            if (weapon == null)
                weapon = impulseGun;

            if (switchTimer > 0)
                switchTimer -= Time.deltaTime;

            if (switchTimer < 0)
                switchTimer = 0;

            beamGun.Update();
            impulseGun.Update();
            rocketGun.Update();
            streamGun.Update();
            photonGun.Update();

            if (bdrone != null && bdrone.isSelf && bammoProgressBar == null)
            {
                var ammoProgressBar = GameObject.Find("AmmoProgressBar"); 

                if (ammoProgressBar != null)
                    bammoProgressBar = ammoProgressBar.GetComponent<BProgressBar>();
            }

            if (bammoProgressBar != null)
            {
                bammoProgressBar.Value = (float)impulseGun.charge / (float)impulseGun.maxCharge;
            }

        }

        public void ButtonDownSwitchWeapon(byte weapon)
        {
            photonView.RPC("SwitchWeapon", PhotonTargets.All, new object[]
            {                
                weapon
            });
        }

        public bool SwitchWeapon(byte weapon)
        {
            if (weapons == null)
                weapons = new Weapon[]
                {
                    beamGun,
                    impulseGun,
                    rocketGun,
                    streamGun,
                    photonGun
                };

            return SwitchWeapon(weapons [weapon]);
        }


        public bool SwitchWeapon(Weapon weapon)
        {
            if (weapon != this.weapon && switchTimer <= 0)
            {
                switchTimer = switchTime;

                if (weapon != null && this.weapon != null)
                    weapon.cooldownTimer += this.weapon.cooldownTimer;
                
                this.weapon = weapon;
                return true;
            }

            return false;
        }
    }
}