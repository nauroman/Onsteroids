using System;
using UnityEngine;
using System.Reflection.Emit;
using UnityEngine.UI;

namespace Flashunity.Drones
{
    [Serializable]
    public class InputDriver
    {
        public float XSensitivity = 200;
        public float YSensitivity = 25;

        public float speed = 6f;
        public float linearSpeed = 6f;
        //18f;
        public float linearTime = 0.2f;
        public float linearTimer = 0;
        public float accel = 20;
//50f;
        public float slowDown = 15f;

        float xRot = 0;
        float yRot = 0;

        public bool lockCursor = false;
        private bool cursorIsLocked = true;

        Rigidbody rb;
        Transform drone;
        Weapons weapons;


        public InputDriver(Rigidbody rb, Transform drone, BDrone bdrone)
        {
            this.rb = rb;
            this.drone = drone;
            this.weapons = bdrone.weapons;
        }

        public void Update()
        {
            UpdateCursorLock();

            var dt = Time.deltaTime;
            var velocity = Vector3.zero;
            var linear = linearTimer >= linearTime;

            linearTimer += Time.deltaTime;

            if (cursorIsLocked)
            {
                var mouseX = GetAxis("Mouse X");
                var mouseY = GetAxis("Mouse Y");

                if (mouseX > 0 || mouseY > 0)
                {
                    linear = false;
                    linearTimer = 0;
                }
                
//                var mouseAxises = new Vector2(GetAxis("Mouse X"), GetAxis("Mouse Y"));


                //        yRot += mouseX * XSensitivity * dt;
                xRot += mouseY * YSensitivity * dt;

                xRot = 0;

                if (GetButton("Fire1"))
                    weapons.ButtonFire();

                /*
                if (GetButtonDown("Weapon1"))
                    weapons.ButtonDownSwitchWeapon(0);

                if (GetButtonDown("Weapon2"))
                    weapons.ButtonDownSwitchWeapon(1);

                if (GetButtonDown("Weapon3"))
                    weapons.ButtonDownSwitchWeapon(2);
                
                if (GetButtonDown("Weapon4"))
                    weapons.ButtonDownSwitchWeapon(3);

                if (GetButtonDown("Weapon5"))
                    weapons.ButtonDownSwitchWeapon(4);
                */
            }


            if (xRot > 90)
                xRot = 90;
            else if (xRot < -90)
                xRot = -90;

            rb.rotation = Quaternion.Euler(0, yRot, 0);

            drone.localRotation = Quaternion.Euler(xRot, 0, 0);

            var axises = new Vector3(GetAxis("Horizontal"), GetAxis("Vertical"), GetAxis("Forward")).normalized;

            var rbLocalVelocity = rb.transform.InverseTransformDirection(rb.velocity);

            if (axises.x != 0 || axises.y != 0 || axises.z == 0)
            {
                linear = false;
                linearTimer = 0;
            }

            if (cursorIsLocked)
            {
                yRot += axises.x * XSensitivity * dt;
            }


//            velocity.x = GetAxisVelocity(cursorIsLocked ? axises.x : 0, rbLocalVelocity.x, dt, linear);
            velocity.x = GetAxisVelocity(0, rbLocalVelocity.x, dt, linear);
            //          velocity.y = GetAxisVelocity(cursorIsLocked ? axises.y : 0, rb.velocity.y, dt, linear);
            velocity.z = GetAxisVelocity(cursorIsLocked ? axises.z : 0, rbLocalVelocity.z, dt, linear);

//            velocity.x = 0;
            //          velocity.y = 0;

            rb.AddRelativeForce(velocity, ForceMode.VelocityChange);
//                move = Vector3.zero;


            /*
            vMov.x = GetAxis("Horizontal") * dt * speed;
            vMov.y = GetAxis("Vertical") * dt * speed;
            vMov.z = GetAxis("Forward") * dt * speed;
*/

            //        Debug.Log("forward: " + GetAxis("Forward"));

            //        axis.z = GetAxis("Forward");
//                int newForward = af > 0 ? 1 : af < 0 ? -1 : 0;

            /*
                if (newForward != prevForvard)
                {
                    var a = yRot * Mathf.Deg2Rad;
                    var f = af * dt * speed;

                    var dx = f * Mathf.Sin(a);
                    var dz = f * Mathf.Cos(a);

                    move.x = dx;
                    move.z = dz;

                    if (prevForvard != 0)
                    {
                        move.x += dx;
                        move.z += dz;
                    }

                    prevForvard = newForward;
                }

                var ah = GetAxis("Horizontal");
                int newHorizontal = ah > 0 ? 1 : ah < 0 ? -1 : 0;

                if (newHorizontal != prevHorizontal)
                {
                    var a = (yRot + 90) * Mathf.Deg2Rad;
                    var f = ah * dt * speed;

                    var dx = f * Mathf.Sin(a);
                    var dz = f * Mathf.Cos(a);

                    move.x = dx;
                    move.z = dz;

                    if (prevHorizontal != 0)
                    {
                        move.x += dx;
                        move.z += dz;
                    }

                    prevHorizontal = newHorizontal;
                }
*/
//                ForceMode.

            //rb.rela


//                move.x = f * Mathf.Sin(a);
            //              move.z = f * Mathf.Cos(a);

//                move.x += GetAxis("Horizontal") * Mathf.Sin(Mathf.Deg2Rad * (yRot + 90)) * dt * speed;
            //              move.z += GetAxis("Horizontal") * Mathf.Cos(Mathf.Deg2Rad * (yRot + 90)) * dt * speed;

            //            DOTween.To(x => velocity.x = x, speed, 0, 2f).SetEase(Ease.OutCubic);



            /*
            float axis;

            axis = cursorIsLocked ? GetAxis("Vertical") : 0;

            var rbVelocity = rb.velocity.y;
            var rbSpeed = Math.Abs(rbVelocity);

            //             var verticalSpeed = Math.Abs(rb.velocity.y);

            if (axis == 0 || rbSpeed > speed)
            {
                velocity.y = -rbVelocity * slowDown * dt;
            } else
            {
                if (Math.Abs(rbVelocity) < speed)
                    velocity.y = axis * accel * dt;
                else if (rbVelocity > speed)
                    velocity.y = speed - rbVelocity;
                else if (rbVelocity < -speed)
                    velocity.y = -speed - rbVelocity;
                else
                    velocity.y = 0;
            }
            */



            /*
            axis = cursorIsLocked ? GetAxis("Forward") : 0;

            rbVelocity = rb.transform.InverseTransformDirection(rb.velocity).z;
            rbSpeed = Math.Abs(rbVelocity);
            if (axis == 0 || rbSpeed > speed)
            {
                velocity.z = -rbVelocity * slowDown * dt;
            } else
            {
                if (rbSpeed < speed)
                    velocity.z = axis * accel * dt;
                else if (rbVelocity > speed)
                    velocity.z = speed - rbVelocity;
                else if (rbVelocity < -speed)
                    velocity.z = -speed - rbVelocity;
                else
                    velocity.z = 0;
            }

            axis = cursorIsLocked ? GetAxis("Horizontal") : 0;

            rbVelocity = rb.transform.InverseTransformDirection(rb.velocity).x;
            rbSpeed = Math.Abs(rbVelocity);

            if (axis == 0 || rbSpeed > speed)
            {
                velocity.x = -rbVelocity * slowDown * dt;
            } else
            {
                if (rbSpeed < speed)
                    velocity.x = axis * accel * dt;
                else if (rbVelocity > speed)
                    velocity.x = speed - rbVelocity;
                else if (rbVelocity < -speed)
                    velocity.x = -speed - rbVelocity;
                else
                    velocity.x = 0;
            }

            /*
                var locVel = transform.InverseTransformDirection(rigidbody.velocity);
                locVel.z = MovSpeed;
                rigidbody.velocity = transform.TransformDirection(locVel);
*/

            /*
                var a = yRot * Mathf.Deg2Rad;
                var ax = Mathf.Sin(a);
                var az = Mathf.Cos(a);

                var forward = new Vector3(rb.velocity.x * ax, 0, rb.velocity.z * az);

                var forwardSpeed = forward.magnitude;

                if (forwardSpeed < 0.00001)
                    forwardSpeed = 0;

                //         Debug.Log("forwardSpeed: " + forwardSpeed);

                if (axis == 0)
                {
                    if (forwardSpeed > 0.001f)
                    {
                        var dot = Vector3.Dot(forward.normalized, new Vector3(ax, 0, az).normalized);
                        float dir = dot == 0 ? 0 : dot > 0 ? 1 : -1;


                        if (dir != 0)
                        {
//                    velocity.x = -1 * slowDown * dt / (rb.velocity.x * ax);
                            //                  velocity.z = -1 * slowDown * dt / (rb.velocity.z * az);

                            velocity.x = dir * forwardSpeed * ax * slowDown * dt;
                            velocity.z = dir * forwardSpeed * az * slowDown * dt;
                        }
                    }
                } else
                {
                    var f = axis * accel * dt;

//                    var forwardSpeed = new Vector3(rb.velocity.x * ax, 0, rb.velocity.z * az).magnitude;
                    var vd = new Vector3(f * ax, 0, f * az);

                    if (forwardSpeed < speed)
                    {
                        velocity.x = vd.x;
                        velocity.z = vd.z;
                    }
                }
*/


            /*

                axis = GetAxis("Horizontal");

                a = (yRot + 90) * Mathf.Deg2Rad;
                ax = Mathf.Sin(a);
                az = Mathf.Cos(a);

                forward = new Vector3(rb.velocity.x * ax, 0, rb.velocity.z * az);

                forwardSpeed = forward.magnitude;

                if (forwardSpeed < 0.00001)
                    forwardSpeed = 0;

                //         Debug.Log("forwardSpeed: " + forwardSpeed);

                if (axis == 0)
                {
                    if (forwardSpeed > 0.001f)
                    {
                        var dot = Vector3.Dot(forward.normalized, new Vector3(ax, 0, az).normalized);
                        float dir = dot == 0 ? 0 : dot > 0 ? 1 : -1;


                        if (dir != 0)
                        {
                            //                    velocity.x = -1 * slowDown * dt / (rb.velocity.x * ax);
                            //                  velocity.z = -1 * slowDown * dt / (rb.velocity.z * az);

                            velocity.x += dir * forwardSpeed * ax * slowDown * dt;
                            velocity.z += dir * forwardSpeed * az * slowDown * dt;
                        }
                    }
                } else
                {
                    var f = axis * accel * dt;

                    //                    var forwardSpeed = new Vector3(rb.velocity.x * ax, 0, rb.velocity.z * az).magnitude;
                    var vd = new Vector3(f * ax, 0, f * az);

                    if (forwardSpeed < speed)
                    {
                        velocity.x += vd.x;
                        velocity.z += vd.z;
                    }
                }
                */

            //         prevAxis = axis;
                    

            //      Debug.Log("velocity.y: " + velocity.y);

//                move.y = GetAxis("Vertical") * dt * speed;


//                rb.AddForce(velocity, ForceMode.VelocityChange);

            /*
                if (rb.velocity.magnitude > speed)
                {
                    rb.velocity = rb.velocity.normalized * speed;
                }
                */
//                rb.velo
//                rb.getr

//            UpdateRB(new Vector3(xRot, yRot, 0), vMov);
//                UpdateRB(move);


            //          rb.AddRelativeTorque();

//            rbDriver.Update(new Vector3(xRot, yRot, 0), move);


//            UpdateRigidBody(rotation, move);
//            move = vMov;

            //          rotation = Quaternion.AngleAxis(angle, axis);
//            rb.MovePosition(q * (rb.transform.position - origin) + origin);
            //          rb.MoveRotation(rb.transform.rotation * q);

//            lookAt.x += xRot;
            //          lookAt.y += xRot;
//            = new Vector3(xRot, yRot, 0);
        }

        float GetAxisVelocity(float axis, float rbVelocity, float dt, bool linear)
        {
            float velocity;

            /*
            if (axis == 0)
            {
                velocity = -rbVelocity * slowDown * dt;
            } else
            {
            */
            var s = linear ? linearSpeed : speed;
            velocity = axis * accel * dt;

            if ((rbVelocity + velocity) > s)
                velocity = s - rbVelocity;
            else if ((rbVelocity + velocity) < -s)
                velocity = -s - rbVelocity;
            //      }

            return velocity;
        }

        Quaternion ClampRotationAroundXAxis(Quaternion q)
        {
            q.x /= q.w;
            q.y /= q.w;
            q.z /= q.w;
            q.w = 1.0f;

            float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan(q.x);

            angleX = Mathf.Clamp(angleX, -89, 89);

            q.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleX);

            return q;
        }

        /*
        void AccelVertical(float a)
        {
//            DOTween.Kill(2);

            velocity.y = -rb.velocity.y * slowDown;
        }


        void SlowdownVertical(float a)
        {
            //DOTween.Kill(2);

            velocity.y = -rb.velocity.y * slowDown;
        }


        void StartVertical(float vel)
        {
            velocity.y = vel;

            DOTween.Kill(2);

            Sequence sequenceVertical = DOTween.Sequence();

            sequenceVertical.Append(DOTween.To(() => velocity.y, x => velocity.y = x, 0, speedTime)).SetEase(Ease.OutSine);

            sequenceVertical.id = 2;
        }
*/

        /*
        void StopVertical()
        {
//            rb.velocity.y;

//            DOTween.Kill(velocity.y);
            DOTween.To(x => velocity.y = x, speed, -rb.velocity.y / 2, 1f).SetEase(Ease.OutCubic);

        }
        */

        //        void UpdateRB(Vector3 rotation, Vector3 move)



        public bool GetButton(string name)
        {
            return Input.GetButton(name);
        }


        public bool GetButtonDown(string name)
        {
            return Input.GetButtonDown(name);
        }

        public bool GetButtonUp(string name)
        {
            return Input.GetButtonUp(name);
        }

        public float GetAxis(string name)//, bool raw)
        {
            return Input.GetAxisRaw(name);// : Input.GetAxis(name);
        }

        public void SetCursorLock(bool value)
        {
            /*
            lockCursor = value;
            if (!lockCursor)
            {//we force unlock the cursor if the user disable the cursor locking helper
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            */
        }

        public void UpdateCursorLock()
        {
            cursorIsLocked = true;

            /*
            if (lockCursor)
                InternalLockUpdate();
                */
        }

        /*
        private void InternalLockUpdate()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                cursorIsLocked = false;
            } else if (Input.GetMouseButtonUp(0))
            {
                cursorIsLocked = true;
            }

            if (cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            } else if (!cursorIsLocked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
        }
        */

    }
}