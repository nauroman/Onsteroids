using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Flashunity.Drones
{
    public class BCanvasPractice : MonoBehaviour
    {
        BDrone bdrone;
        Weapons weapons;
        BDestructable bdestructable;

        [SerializeField]
        BProgressBar bprogressBarAmmo;

        [SerializeField]
        BProgressBar bprogressBarHealth;

        public void LoadLobby()
        {
            SceneManager.LoadScene("lobby");
        }

        void Awake()
        {
            bdrone = GameObject.FindObjectOfType<BDrone>();

            if (bdrone)
            {
                weapons = bdrone.weapons;
                bdestructable = bdrone.GetComponent<BDestructable>();
            }
        }

        void Update()
        {
            if (weapons != null && weapons.weapon != null)
            {
                bprogressBarAmmo.TextValue = weapons.weapon.charge.ToString();
                bprogressBarAmmo.Value = (float)weapons.weapon.charge / (float)weapons.weapon.maxCharge;
            }

            if (bdestructable)
            {
                bprogressBarHealth.TextValue = bdestructable.health.ToString();
                bprogressBarHealth.Value = (float)bdestructable.health / 100f;
            }
        }
    }
}
