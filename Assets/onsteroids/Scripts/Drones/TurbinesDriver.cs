using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Flashunity.Drones
{
    [Serializable]
    public class TurbinesDriver
    {
        public Rigidbody rb;
        //        public Transform drone;

        public Transform turbineL;
        public Transform turbineR;
        public Transform fireL;
        public Transform fireR;

        float maxScale = 2;

        //        public float prevRotY = float.MinValue;


        public void Update(Vector3 rbLocalVelocity)
        {
            //          if (prevRotY == float.MinValue)
            //            prevRotY = rb.rotation.eulerAngles.y;

            //        var rbLocalVelocity = rb.transform.InverseTransformDirection(velocity);

            var front = Math.Abs(rbLocalVelocity.z);
            var up = Math.Abs(rbLocalVelocity.y);

            var maxFrontUp = Math.Max(front, up);

            var left = Math.Max(rbLocalVelocity.x, maxFrontUp) * 0.2f;
            var right = Math.Max(-rbLocalVelocity.x, maxFrontUp) * 0.2f;
          
            if (left > maxScale)
                left = maxScale;

            if (right > maxScale)
                right = maxScale;

            fireL.localScale = new Vector3(left, 1, 1);
            fireR.localScale = new Vector3(right, 1, 1);
//            var frontup = front > 0.1f || up > 0.1f;

            //          var fL = rbLocalVelocity.x > 0.1f || frontup;
            //        var fR = rbLocalVelocity.x < -0.1f || frontup;

            //      fireL.gameObject.SetActive(fL);
            //    fireR.gameObject.SetActive(fR);
            
            var rotZ = -rbLocalVelocity.z * 4;
            var rotY = -rbLocalVelocity.y * 5;


            var rotX = rbLocalVelocity.x * 2.5f;


            if (rotZ > 80)
                rotZ = 80;
            else if (rotZ < -70)
                rotZ = -70;

            //- drone.localRotation.eulerAngles.x

//            rb.ang

            var rbRotY = rb.rotation.eulerAngles.y;

            /*
            var dRotY = (rbRotY - prevRotY) * 10;

            if (dRotY > 30)
                dRotY = 30;
            else if (dRotY < -30)
                dRotY = -30;

            // !!!!!!!!!!!!!
            dRotY = 0;
*/
            turbineL.rotation = Quaternion.Euler(0, -20 + rotZ + rbRotY, 30 - rotY - rotX);
            turbineR.rotation = Quaternion.Euler(0, 20 - rotZ + rbRotY, -30 + rotY - rotX);

            //          prevRotY = rbRotY;
//            turbineL.rot(, 0, 0);
//            turbineR.localRotation = Quaternion.Euler(-90, rotY, -rotZ);

        }
    }


}