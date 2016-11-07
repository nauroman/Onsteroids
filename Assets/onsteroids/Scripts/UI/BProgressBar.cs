using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Flashunity.Drones
{
    public class BProgressBar : MonoBehaviour
    {
        [SerializeField]
        Text textValue;

        [SerializeField]
        Text textLabel;

        [SerializeField]
        Image imageProgress;

        public float Value
        {
            get
            {
                return imageProgress.fillAmount;
            }
            set
            {
                imageProgress.fillAmount = value;

            }
        }

        public string TextValue
        {
            get
            {
                return textValue.text;
            }
            set
            {
                textValue.text = value;
            }
        }

        public string Label
        {
            get
            {
                return textLabel.text;
            }
            set
            {
                textLabel.text = value;
            }
        }
    }
}