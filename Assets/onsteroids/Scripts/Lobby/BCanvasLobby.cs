using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flashunity.Drones
{
    public class BCanvasLobby : Photon.MonoBehaviour
    {
        public void LoadPractice()
        {
            Debug.Log("LoadPractice");

            SceneManager.LoadScene("practice");
        }

        public void LoadDesert()
        {
            Debug.Log("LoadDesert");

            SceneManager.LoadScene("desert");
        }

    }
}