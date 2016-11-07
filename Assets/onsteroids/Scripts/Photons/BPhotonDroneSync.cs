//
// A (very) simple network interpolation script, using Lerp().
//
// This will lag-behind, compared to the moving cube on the controlling client.
// Actually, we deliberately lag behing a bit more, to avoid stops, if updates arrive late.
//
// This script does not hide loss very well and might stop the local cube.
//

using UnityEngine;

namespace Flashunity.Drones
{

    [RequireComponent(typeof(PhotonView))]
    public class BPhotonDroneSync : Photon.MonoBehaviour, IPunObservable
    {
        Vector3 rbPos;
        //        Vector3 rbAndDroneRot;
        Vector3 rbVelocity;
        //      Vector3 velocity;

        Vector3 onUpdatePos;
        Vector3 onUpdateRot;
        Vector3 onUpdateVelocity;
        float fraction;

        Quaternion rbRotFrom;
        Quaternion rbRotTo;
        Quaternion droneRotFrom;
        Quaternion droneRotTo;

        Rigidbody rb;
        public Transform droneRot;

        public BDrone bdrone;

        //        public TurbinesDriver turbinesDriver;
        //      public WingsDriver wingsDriver;


        public void Start()
        {
            rb = GetComponent<Rigidbody>();

            rbPos = rb.position;
            onUpdatePos = rb.position;
            //           rbAndDroneRot = new Vector3(droneRot.localRotation.eulerAngles.x, rb.rotation.eulerAngles.y, 0);
            onUpdateRot = new Vector3(droneRot.localRotation.eulerAngles.x, rb.rotation.eulerAngles.y, 0);

            rbRotFrom = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0);
            rbRotTo = Quaternion.Euler(0, rb.rotation.eulerAngles.y, 0);

            droneRotFrom = Quaternion.Euler(droneRot.localRotation.eulerAngles.x, 0, 0);
            droneRotTo = Quaternion.Euler(droneRot.localRotation.eulerAngles.x, 0, 0);

            rbVelocity = rb.velocity;
            onUpdateVelocity = rb.velocity;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.isWriting)
            {
                if (rb != null && droneRot != null)
                {
                    Vector3 pos = rb.position;
                    Vector3 velocity = rb.velocity;
                    float rbRotY = rb.rotation.eulerAngles.y;
                    float rotX = droneRot.localRotation.eulerAngles.x;

                    stream.Serialize(ref pos);
                    stream.Serialize(ref velocity);
                    stream.Serialize(ref rbRotY);
                    stream.Serialize(ref rotX);
                }
            } else
            {
                //          Vector3 pos = Vector3.zero;
                //            Vector3 velocity = Vector3.zero;
//                float rotY = 0;
                //              float rotX = 0;

                onUpdatePos = rb.position;
                onUpdateVelocity = rbVelocity;
                onUpdateRot = new Vector3(droneRot.localRotation.eulerAngles.x, rb.rotation.eulerAngles.y, 0);
                float rbRotY = 0;
                float rotX = 0;

                stream.Serialize(ref rbPos);
                stream.Serialize(ref rbVelocity);
                stream.Serialize(ref rbRotY);
                stream.Serialize(ref rotX);

                rbRotFrom = Quaternion.Euler(0, onUpdateRot.y, 0);
                rbRotTo = Quaternion.Euler(0, rbRotY, 0);

                if (Vector3.Distance(onUpdatePos, rbPos) > 3)
                {
                    onUpdatePos = rbPos;
                }

                droneRotFrom = Quaternion.Euler(onUpdateRot.x, 0, 0);
                droneRotTo = Quaternion.Euler(rotX, 0, 0);

//                rbPos = pos;
                //              rbRotY = rotY;
//                rbVelocity = 
                //              this.rotX = rotX;


                //         latestCorrectPos = pos;                
                fraction = 0;                          
                /*
                if (rb != null)
                {
                    velocity -= rb.velocity;

                    rb.AddForce(velocity, ForceMode.VelocityChange);
                }
                */

            }
        }


        [PunRPC]
        public void SwitchWeapon(byte weapon)
        {
            bdrone.weapons.SwitchWeapon(weapon);
        }

        [PunRPC]
        public void FireBeam(Vector3 at)
        {
            bdrone.weapons.FireBeam(at);
        }

        [PunRPC]
        public void FireImpulse(Vector3 at, byte id)
        {
            bdrone.weapons.FireImpulse(at, id);
        }

        [PunRPC]
        public void FireRocket(Vector3 at, byte id)
        {
            bdrone.weapons.FireRocket(at, id);
        }

        [PunRPC]
        public void FireStream(Vector3 at)
        {
            bdrone.weapons.FireStream(at);
        }

        [PunRPC]
        public void FirePhoton(Vector3 at)
        {
            bdrone.weapons.FirePhoton(at);
        }

        [PunRPC]
        public void HitBeam(Vector3 position, byte armorPart)
        {
            bdrone.ShowArmor(armorPart);
            bdrone.Damage(bdrone.weapons.beamGun.damage);
        }

        [PunRPC]
        public void HitImpulse(Vector3 position, byte armorPart, short fromPhotonViewId, byte fireId)
        {
            bdrone.ShowArmor(armorPart);
            bdrone.Damage(bdrone.weapons.impulseGun.damage);

            var photonView = PhotonView.Find(fromPhotonViewId);

            if (photonView)
            {
                var bdrone = photonView.GetComponent<BDrone>();        

                bdrone.weapons.impulseGun.DestroyFire(fireId);
            }
        }

        [PunRPC]
        public void HitRocket(Vector3 position, byte armorPart, short fromPhotonViewId, byte fireId, byte damage)
        {
            bdrone.ShowArmor(armorPart);
            bdrone.Damage(bdrone.weapons.rocketGun.damage);

            var photonView = PhotonView.Find(fromPhotonViewId);

            if (photonView)
            {
                var bdrone = photonView.GetComponent<BDrone>();

                bdrone.weapons.rocketGun.DestroyFire(fireId);
            }
        }

        [PunRPC]
        public void HitStream(Vector3 position, byte armorPart)
        {
            bdrone.ShowArmor(armorPart);
            bdrone.Damage(bdrone.weapons.streamGun.damage);

        }

        [PunRPC]
        public void HitPhoton(Vector3 position, byte armorPart)
        {
            bdrone.ShowArmor(armorPart);
            bdrone.Damage(bdrone.weapons.photonGun.damage);

        }

        public void Update()
        {
            if (!this.photonView.isMine)
            {
                if (rb != null && droneRot != null)
                {
                    /*
                    if (Vector3.Distance(rb.position, rbPos) < 0.001f)
                    {
                        rb.AddForce(-rb.velocity, ForceMode.VelocityChange);
                    }
                    */


//                    rb.position = rbPos;


                    rb.isKinematic = true;

                    fraction = fraction + Time.deltaTime * ((100 / PhotonNetwork.sendRate) - 1);
//                    rb.rotation = Quaternion.Lerp(Quaternion.Euler(0, onUpdateRot.y, 0), Quaternion.Euler(0, rbAndDroneRot.y, 0), fraction);
                    //                  droneRot.localRotation = Quaternion.Lerp(Quaternion.Euler(onUpdateRot.x, 0, 0), Quaternion.Euler(rbAndDroneRot.x, 0, 0), fraction);

                    rb.rotation = Quaternion.Lerp(rbRotFrom, rbRotTo, fraction);
                    droneRot.localRotation = Quaternion.Lerp(droneRotFrom, droneRotTo, fraction);

                    rb.position = Vector3.Lerp(onUpdatePos, rbPos, fraction);

                    var velocity = Vector3.Lerp(onUpdateVelocity, rbVelocity, fraction);

                    var rbLocalVelocity = rb.transform.InverseTransformDirection(velocity);

                    bdrone.UpdateRbLocalVelocity(rbLocalVelocity);
//                    bdrone.turbinesDriver.Update(velocity);
                    //                  bdrone.wingsDriver.Update(velocity);

//                    rbVelocity = Vector3.Lerp(onUpdateVelocity, rbVelocity, fraction);
                    //        rb.isKinematic = false;

                }
            }
            /*
        if (this.photonView.isMine)
        {
            return;     // if this object is under our control, we don't need to apply received position-updates 
        }

        // We get 10 updates per sec. Sometimes a few less or one or two more, depending on variation of lag.
        // Due to that we want to reach the correct position in a little over 100ms. We get a new update then.
        // This way, we can usually avoid a stop of our interpolated cube movement.
        //
        // Lerp() gets a fraction value between 0 and 1. This is how far we went from A to B.
        //
        // So in 100 ms, we want to move from our previous position to the latest known. 
        // Our fraction variable should reach 1 in 100ms, so we should multiply deltaTime by 10.
        // We want it to take a bit longer, so we multiply with 9 instead!

        this.fraction = this.fraction + Time.deltaTime * 9;
        transform.localPosition = Vector3.Lerp(this.onUpdatePos, this.latestCorrectPos, this.fraction); // set our pos between A and B
        */
        }
    }
}