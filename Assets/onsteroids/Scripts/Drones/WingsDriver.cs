using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Flashunity.Drones
{
    [Serializable]
    public class WingsDriver
    {
        public Rigidbody rb;

        public Transform wingL;
        public Transform wingR;

        public void Update(Vector3 rbLocalVelocity)
        {
            //       var rbLocalVelocity = rb.transform.InverseTransformDirection(velocity);

            //      var front = Math.Abs(rbLocalVelocity.z);
            //       var up = Math.Abs(rbLocalVelocity.y);

            //        var maxFrontUp = Math.Max(front, up);

//            var left = Math.Max(rbLocalVelocity.x, maxFrontUp);
            //          var right = Math.Max(-rbLocalVelocity.x, maxFrontUp);

            var rotZ = -rbLocalVelocity.z * 6;
            var rotY = -rbLocalVelocity.y * 10;

            var rotX = rbLocalVelocity.x * 2.5f;
//            var rotYR = -rbLocalVelocity.x * 2;


            if (rotZ > 55)
                rotZ = 55;
            else if (rotZ < -50)
                rotZ = -50;


            /*
            if (rotZ > 60)
                rotZ = 60;
            else if (rotZ < -60)
                rotZ = -60;
*/
            //    wingL.localRotation = Quaternion.Euler(0, -20 + rotZ, 30 - rotY);
            //  wingR.localRotation = Quaternion.Euler(0, 20 - rotZ, -30 + rotY);

            var rbRotY = rb.rotation.eulerAngles.y;

            //        var dRotY = (rbRotY - prevRotY) * 10;


            wingL.rotation = Quaternion.Euler(-90 - rotY - rotX, 90 + (rbRotY + 180), 90 + rotZ);
            wingR.rotation = Quaternion.Euler(-90 + rotY - rotX, 90 + (rbRotY + 180), 90 - rotZ);

//            wingL.rotation = Quaternion.Euler(-90 - rotY, 90 + rotZ + rb.rotation.eulerAngles.y, -90);
            //          wingR.rotation = Quaternion.Euler(-90 + rotY, 90 - rotZ + rb.rotation.eulerAngles.y, -90);

        }
    }
}