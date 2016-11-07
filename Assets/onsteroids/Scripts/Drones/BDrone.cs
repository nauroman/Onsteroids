using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using System;

namespace Flashunity.Drones
{
    public class BDrone : BDestructable
    {
        public bool offline;

        public Transform drone;
               
        public Rigidbody rb;

        [SerializeField]
        SphereCollider sphereCollider;

        InputDriver inputDriver;

        [SerializeField]
        public WingsDriver wingsDriver;

        [SerializeField]
        public TurbinesDriver turbinesDriver;

        [SerializeField]
        public GunDriver gunDriver;

        [SerializeField]
        public Weapons weapons;

        public Light averageLight;

        //        public int health = 100;
        public int armor = 100;

        public bool isSelf;

        void Start()
        {
            Debug.Log("Start");

            if (offline)
                SetSelf();
        }

        public void SetSelf()
        {
            inputDriver = new InputDriver(rb, drone, this);

            isSelf = true;
//            bammoProgressBar = GameObject.Find("ammoProgressBar") as BProgressBar;

//            bammoProgressBar = GameObject.FindObjectOfType<("ammoProgressBar") as >BProgressBar;

            Debug.Log("inputDriver");
        }

        public void ShowArmor(byte armorPart)
        {
            
        }

        public void Damage(short damage)
        {
            if (health > 0)
            {
                if (audioSourceHit)
                {
                    audioSourceHit.Play();
                }

                health -= damage;

                if (health <= 0)
                {
                    health = 0;
                    Destruct();     
                }
            }

            /*
            if (armor > 0)
            {
                armor -= damage;

                if (armor < 0)
                {
                    health -= armor;
                }

            } else
            {
                health -= damage;
            }

            if (health <= 0)
            {
                Destroy(gameObject);
            }
            */

        }

        void FixedUpdate()
        {
            var rbLocalVelocity = rb.transform.InverseTransformDirection(rb.velocity);
            UpdateRbLocalVelocity(rbLocalVelocity);
        }


        public void UpdateRbLocalVelocity(Vector3 rbLocalVelocity)
        {
            wingsDriver.Update(rbLocalVelocity);
            turbinesDriver.Update(rbLocalVelocity);
        }

        void Update()
        {
            if (inputDriver != null)
                inputDriver.Update();

            weapons.Update();
        }

        override public bool Collision(int collisionDamage)
        {
            Debug.Log("BDrone.Collision");

            if (audioSourceCollision)
            {
                audioSourceCollision.time = 2.5f;
                audioSourceCollision.Play();
                StartCoroutine(StopCollisionSound());
            }

            if (health > 0)
            {
                health -= collisionDamage;

                if (health <= 0)
                {
                    health = 0;
                    Destruct();     
                    return true;
                }
            }
            return false;
        }

        IEnumerator StopCollisionSound()
        {
            yield return new WaitForSeconds(0.6f);
            audioSourceCollision.Stop();
        }

        override protected void Destruct()
        {
            if (audioSourceDestroy)
                audioSourceDestroy.Play();

            if (explosion)
                explosion.Play();

            inputDriver = null;
//            sphereCollider.enabled = false;

            /*
            if (audioSourceDestroy || explosion)
                StartCoroutine(DestroyThis(2));
            else
                Destroy(gameObject);
                */
        }

        IEnumerator DestroyThis(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);            
        }
            
    }
}