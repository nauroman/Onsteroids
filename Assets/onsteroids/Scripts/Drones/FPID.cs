using UnityEngine;
using System.Collections;

namespace Flashunity.Drones
{
    public class FPID
    {
        float Kp = 0.2f;
        float Ki = 0.0f;
        float Kd = 0.1f;
		
        private float P, I, D;
        private float prevError;

        public float GetOutput(float currentError, float deltaTime)
        {
            P = currentError;
            I += P * deltaTime;
            D = (P - prevError) / deltaTime;
            prevError = currentError;

            return P * Kp + I * Ki + D * Kd;
        }
    }
}
