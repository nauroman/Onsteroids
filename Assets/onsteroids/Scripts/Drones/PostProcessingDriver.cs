using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.PostProcessing;

namespace Flashunity.Drones
{
    public class PostProcessingDriver
    {
        PostProcessingBehaviour postProcessingBehaviour;

        public PostProcessingDriver(PostProcessingBehaviour postProcessingBehaviour)
        {
            this.postProcessingBehaviour = postProcessingBehaviour;
        }

        public void Update(Vector3 rbLocalVelocity, float maxRegularSpeed)
        {
            var magnitude = rbLocalVelocity.magnitude;

            var settings = new ChromaticAberrationModel.Settings();

            settings.intensity = 0.3f;

            if (magnitude > maxRegularSpeed)
                settings.intensity += (magnitude - maxRegularSpeed) / 3;

            postProcessingBehaviour.profile.chromaticAberration.settings = settings;
        }
    }
}
