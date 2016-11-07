using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Flashunity.Drones
{
    public class BPhotonMap : Photon.MonoBehaviour
    {
        [SerializeField]
        BCanvasMap bcanvasMap;

        [SerializeField]
        string roomName;

        [SerializeField]
        GameObject buttonMenu;



        void ClearScene()
        {
            var setupGo = GameObject.Find("DroneSetup");

            if (setupGo != null)
                Destroy(setupGo);

            setupGo = GameObject.Find("ImpulseSetup");

            if (setupGo != null)
                Destroy(setupGo);

        }

        void Start()
        {
            ClearScene();

            buttonMenu.SetActive(false);

            if (!PhotonNetwork.connected)
                PhotonNetwork.ConnectUsingSettings("0.0.1");
            else
                JoinOrCreateRoom();
        }

        public virtual void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");
            JoinOrCreateRoom();
        }

        public virtual void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
            JoinOrCreateRoom();
        }

        void JoinOrCreateRoom()
        {
            Debug.Log("JoinOrCreateRoom");

            if (PhotonNetwork.connected)
            {
                PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions()
                {
                    IsVisible = true,
                    MaxPlayers = 2
                }, TypedLobby.Default);
            }
        }

        public void OnJoinedRoom()
        {
            Debug.Log("OnJoinedRoom");

            float dx = (PhotonNetwork.room.playerCount - 1) * 2;

            var drone = PhotonNetwork.Instantiate("Drone", new Vector3(dx, 2, 0), Quaternion.Euler(0, 0, 0), 0);

            var bdrone = drone.GetComponent<BDrone>();

            bdrone.SetSelf();

            buttonMenu.SetActive(true);

            bcanvasMap.bdrone = bdrone;
            bcanvasMap.weapons = bdrone.weapons;
        }

        public void LeaveRoom()
        {
            Debug.Log("LeaveRoom");

            buttonMenu.SetActive(false);

            if (PhotonNetwork.inRoom)
                PhotonNetwork.LeaveRoom();
        }



    }
}