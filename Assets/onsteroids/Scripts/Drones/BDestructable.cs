using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flashunity.Drones
{
    public class BDestructable : MonoBehaviour
    {
        [SerializeField]
        protected AudioSource audioSourceCollision;
        [SerializeField]
        protected AudioSource audioSourceHit;
        [SerializeField]
        protected AudioSource audioSourceDestroy;

        protected ParticleSystem explosion;

        //        [SerializeField]
        public int health;

        //        [SerializeField]
        public int collisionDamage;

        void Awake()
        {
            explosion = transform.GetComponentInChildren<ParticleSystem>();
        }

        public bool HitImpulse()
        {
            if (audioSourceHit)
                audioSourceHit.Play();

            if (health > 0)
            {                
                health -= 1;

                if (health <= 0)
                {
                    Destruct();		
                    return true;
                }
            }
            return false;
        }

        public virtual bool Collision(int collisionDamage)
        {
//            if (audioSourceCollision)
            //              audioSourceCollision.Play();

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

        protected virtual void Destruct()
        {
            if (audioSourceDestroy)
                audioSourceDestroy.Play();

            if (explosion)
                explosion.Play();

            if (audioSourceDestroy || explosion)
                StartCoroutine(DestroyThis(5));
            else
                Destroy(gameObject);

        }

        IEnumerator DestroyThis(float time)
        {
            yield return new WaitForSeconds(time);
            Destroy(gameObject);            
        }

        void OnCollisionEnter(Collision collision)
        {
            var bdestructable = collision.transform.root.GetComponent<BDestructable>();

            if (bdestructable)
            {
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
                }
            }
        }

    }
}