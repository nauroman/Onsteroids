using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flashunity.Drones
{
    public class BCanvasMap : MonoBehaviour
    {
        [HideInInspector]
        public BDrone bdrone;

        [HideInInspector]
        public Weapons weapons;
        //        public BDestructable bdestructable;

        [SerializeField]
        BProgressBar bprogressBarAmmo;

        [SerializeField]
        BProgressBar bprogressBarHealth;

        public void LoadLobby()
        {
            SceneManager.LoadScene("lobby");
        }

        void Update()
        {
            if (weapons != null && weapons.weapon != null)
            {
                bprogressBarAmmo.gameObject.SetActive(true);
                bprogressBarAmmo.TextValue = weapons.weapon.charge.ToString();
                bprogressBarAmmo.Value = (float)weapons.weapon.charge / (float)weapons.weapon.maxCharge;
            } else
            {
                bprogressBarAmmo.gameObject.SetActive(false);
            }

            if (bdrone)
            {
                bprogressBarHealth.gameObject.SetActive(true);
                bprogressBarHealth.TextValue = bdrone.health.ToString();
                bprogressBarHealth.Value = (float)bdrone.health / 100f;
            } else
            {
                bprogressBarHealth.gameObject.SetActive(false);

            }
        }


    }
}
