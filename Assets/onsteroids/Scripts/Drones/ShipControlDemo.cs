using UnityEngine;
using System.Collections;

// Here are some sets of gains for the two PID controllers which
// impart different control behaviors to the ship.
//
// -----------------------------+
//   Critically Damped Controls*
// -----------------------------+
// Angle Controller:
// Kp = 9.244681
// Ki = 0
// Kd = 0.06382979
//
// Angular Velocity Controller:
// Kp = 33.7766
// Ki = 0
// Kd = 0.2553191
//
// *Or, at least this is close to critically damped.
//  It's very difficult to remove all oscillation.
//
//
// -----------------------------+
//     Fast Controls
// -----------------------------+
// Angle Controller:
// Kp = 33.51064
// Ki = 0
// Kd = 0.02127661
//
// Angular Velocity Controller:
// Kp = 46.54256
// Ki = 0
// Kd = 0.1808511
//
//
// -----------------------------+
//     Spongy Controls
// -----------------------------+
// Angle Controller:
// Kp = 0.7093059
// Ki = 0
// Kd = 0
//
// Angular Velocity Controller:
// Kp = 11.17021
// Ki = 0
// Kd = 0
//
//
// -----------------------------+
//     Springy Controls
// -----------------------------+
// Angle Controller:
// Kp = 0.7093059
// Ki = 0
// Kd = 0.1648936
//
// Angular Velocity Controller:
// Kp = 0
// Ki = 0
// Kd = 0
//

public class ShipControlDemo : MonoBehaviour
{
    public float TurnSpeed = 500;
    public float MaxAngularVelocity = 20;
	
    private float targetAngleY;
    private float targetAngleX;

    private PID angleController;
    private PID angularVelocityController;

    private PID angleControllerX;
    private PID angularVelocityControllerX;

    private Vector3 torque;
    Rigidbody rb;

    void Awake()
    {
        rb = gameObject.GetComponent<Rigidbody>();

        angleController = gameObject.GetComponents<PID>() [0];
        angularVelocityController = gameObject.GetComponents<PID>() [1];
        angleControllerX = gameObject.GetComponents<PID>() [2];
        angularVelocityControllerX = gameObject.GetComponents<PID>() [3];
        rb.maxAngularVelocity = MaxAngularVelocity;
        targetAngleY = transform.eulerAngles.y;
        targetAngleX = transform.eulerAngles.x;
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
		
        /*************************************************************************************
		* Space Ship Control Demo
		* 
		* This program uses two PID controllers to calculate the torque force required to 
		* steer a ridigdbody space ship towards a target angle. One PID controller corrects 
		* for ship's angle while the other one corrects for the ship's angular velocity.
		*************************************************************************************/
		
        // The target angle is the user-driven angle that the ship will be (hopefully) 
        // steered towards. The target angle is represented in Unity's scene view by a 
        // white line.
        targetAngleY += Input.GetAxis("Horizontal") * TurnSpeed * dt;
        targetAngleX += Input.GetAxis("Vertical") * TurnSpeed * dt;
		
        // The angle controller drives the ship's angle towards the target angle.
        // This PID controller takes in the error between the ship's rotation angle 
        // and the target angle as input, and returns a signed torque magnitude.
        float angleError = Mathf.DeltaAngle(transform.eulerAngles.y, targetAngleY);
        float torqueCorrectionForAngle = angleController.GetOutput(angleError, dt);

        float angleErrorX = Mathf.DeltaAngle(transform.eulerAngles.x, targetAngleX);
        float torqueCorrectionForAngleX = angleControllerX.GetOutput(angleErrorX, dt);

        // The angular veloicty controller drives the ship's angular velocity to 0.
        // This PID controller takes in the negated angular velocity of the ship, 
        // and returns a signed torque magnitude.
        float angularVelocityError = -rb.angularVelocity.y;
        float torqueCorrectionForAngularVelocity = angularVelocityController.GetOutput(angularVelocityError, dt);

        float angularVelocityErrorX = -rb.angularVelocity.x;
        float torqueCorrectionForAngularVelocityX = angularVelocityControllerX.GetOutput(angularVelocityErrorX, dt);

        // The total torque from both controllers is applied to the ship. If we've got 
        // our gains right, then this force will correctly steer the ship to the target 
        // angle and try to hold it there. The torque vector is represented in Unity's 
        // scene view by a red line.
        torque = transform.up * (torqueCorrectionForAngle + torqueCorrectionForAngularVelocity);
        rb.AddTorque(torque);

        torque = transform.right * (torqueCorrectionForAngleX + torqueCorrectionForAngularVelocityX);
        rb.AddTorque(torque);

        /*************************************************************************************/
        // End: Space Ship Control Demo
        /*************************************************************************************/
    }

    void Update()
    {
        DrawDebugStuff();
    }

    void DrawDebugStuff()
    {
        /****************************
		 Debug Stuff (as advertised)
		*****************************/
        Vector3 vectorToTarget = new Vector3(Mathf.Sin(targetAngleY * Mathf.Deg2Rad), 0, Mathf.Cos(targetAngleY * Mathf.Deg2Rad));//Vector3.Normalize(Target.transform.position - transform.position);
        Vector3 targetCrossForward = Vector3.Cross(vectorToTarget, transform.forward);
        float targetDotForward = Mathf.Clamp(Vector3.Dot(vectorToTarget, transform.forward), -1, 1);
        Debug.DrawLine(transform.position, transform.position + vectorToTarget * 6, Color.white);
        Debug.DrawLine(transform.position, transform.position + transform.forward * 5, Color.green);
        Debug.DrawLine(transform.position + transform.forward * 5, transform.position + transform.forward * 5 + transform.position - transform.right * torque.y / 4, Color.red);
    }
}
