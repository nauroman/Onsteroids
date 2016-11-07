using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Flashunity.Drones;
using UnityEngine.Events;
using System;

namespace Flashunity.Drones
{
    public class BAsteroid : BDestructable
    {
        protected Rigidbody rb;
        protected SphereCollider sphereCollider;
        protected GameObject child;
        protected ParticleSystem explosion;

        [SerializeField]
        protected GameObject[] smallerAsteroids;

        void Awake()
        {
            sphereCollider = GetComponent<SphereCollider>();
            rb = GetComponent<Rigidbody>();
            child = transform.GetChild(0).gameObject;
            explosion = transform.GetComponentInChildren<ParticleSystem>();
        }

        override protected void Destruct()
        {
            sphereCollider.enabled = false;
            child.SetActive(false);
            explosion.Play();
            audioSourceDestroy.Play();

            if (smallerAsteroids.Length > 0)
            {
                AddSmallerAsteroids();
            }

            StartCoroutine(DestroyThis());
        }

        IEnumerator DestroyThis()
        {
            yield return new WaitForSeconds(5);
            Destroy(gameObject);            
        }

        void AddSmallerAsteroids()
        {
            var arandom = UnityEngine.Random.Range(-360, 360);

            for (int i = 0; i < smallerAsteroids.Length; i++)
            {
                var asteroid = Instantiate(this.smallerAsteroids [i], transform.position, Quaternion.Euler(UnityEngine.Random.Range(-180, 180), UnityEngine.Random.Range(-180, 180), UnityEngine.Random.Range(-180, 180)));
                var velocity = rb.velocity;

                var basteroid = asteroid.GetComponent<BAsteroid>();
//                basteroid.audioSourceDestroy.Play();

                var distance = basteroid.sphereCollider.radius;

                distance += distance / 10;

                float a = arandom + (360 / smallerAsteroids.Length) * i;
                float arad = Mathf.Deg2Rad * a;
                var dx = Mathf.Cos(arad) * distance;
                var dz = Mathf.Sin(arad) * distance;

                asteroid.transform.position = new Vector3(asteroid.transform.position.x + dx, asteroid.transform.position.y, asteroid.transform.position.z + dz);

                velocity.x += dx * 2;
                velocity.z += dz * 2;

                basteroid.rb.velocity = velocity;
            }
        }

        override public bool Collision(int collisionDamage)
        {
            return false;
        }

        void OnCollisionEnter(Collision collision)
        {
//            return;

            /*
            var bdestructable = collision.transform.root.GetComponent<BDestructable>();
            var bdestructableCollisionDamage = bdestructable.collisionDamage;

            if (bdestructable != null)
            {
                bdestructable.Collision(collisionDamage);

                Collision(bdestructableCollisionDamage);

            } else
            {
                //                Collision(0);
                /*
                var photonView = collision.transform.root.GetComponent<PhotonView>();

                if (photonView != null)
                {
                    byte armorPart = 0;

                    photonView.RPC("HitCollision", PhotonTargets.All, new object[]
                    {                
                        collision.contacts [0].point, armorPart, fromPhotonViewId, fireId
                    });
                }
                */
            //}
        }
    }
}