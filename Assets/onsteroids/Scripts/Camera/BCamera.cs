using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Linq;
using DG.Tweening;

namespace Flashunity.Drones
{
    public class BCamera : MonoBehaviour
    {
        [SerializeField]
        Camera thisCamera;

        float frustumHeight = 0;

        Vector3 positionLobby = new Vector3(-3.44f, 1.1f, 5.83f);
        Vector3 positionMap = new Vector3(0, 13, 0);

        Quaternion rotationLobby = Quaternion.Euler(-12.57f, 124.75f, 0);
        Quaternion rotationMap = Quaternion.Euler(90, 0, 0);

        //        float lerp = 0;

        public float FrustumHeight
        {
            get
            {
                return frustumHeight;
            }
        }

        float frustumWidth = 0;

        public float FrustumWidth
        {
            get
            {
                return frustumWidth;
            }
        }

        void Awake()
        {
//        Debug.Log("mainCamera.rect: " + mainCamera.rect);

            frustumHeight = 2.0f * positionMap.y * Mathf.Tan(thisCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);

            frustumWidth = frustumHeight * thisCamera.aspect;

            frustumWidth /= 2;
            frustumHeight /= 2;
            //var frustumHeight = frustumWidth / mainCamera.aspect;
//            Debug.Log("frustumWidth: " + frustumWidth);
            //          Debug.Log("frustumHeight: " + frustumHeight);

        }

        void Start()
        {
//            lerp = 0;
            transform.position = positionLobby;
            transform.rotation = rotationLobby;

            StartTweeningMap();
        }

        public void StartTweeningMap()
        {
//            transform.position = positionLobby;
            //          transform.rotation = rotationLobby;

            transform.DOMove(positionMap, 3).SetEase(Ease.InOutCirc);
            transform.DORotateQuaternion(rotationMap, 3).SetEase(Ease.InOutCirc);
        }

        public void StartTweeningLobby()
        {
            transform.DOMove(positionLobby, 3).SetEase(Ease.InOutCirc);
            transform.DORotateQuaternion(rotationLobby, 3).SetEase(Ease.InOutCirc).OnComplete(LoadLobby);
        }

        void LoadLobby()
        {
            
        }

        /*
        void Update()
        {
            if (lerp < 1)
            {
                lerp += Time.deltaTime / 5;
                Vector3.Lerp(positionLobby, positionMap, lerp);
                Quaternion.Lerp(rotationLobby, rotationMap, lerp);

                transform.position = Vector3.Lerp(positionLobby, positionMap, lerp);
                transform.rotation = Quaternion.Lerp(rotationLobby, rotationMap, lerp);
            }
        }
        */
            
    }
}