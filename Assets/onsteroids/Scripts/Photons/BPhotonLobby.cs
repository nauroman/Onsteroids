using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace Flashunity.Drones
{
    public class BPhotonLobby : Photon.MonoBehaviour
    {
        [SerializeField]
        GameObject buttonPlay;

        void Start()
        {
            Debug.Log("BPhotonLobby.Start");
            Connect();
        }

        void Connect()
        {
            Debug.Log("PhotonNetwork.connected: " + PhotonNetwork.connected);
            Debug.Log("PhotonNetwork.insideLobby: " + PhotonNetwork.insideLobby);
//            Debug.Log("PhotonNetwork.insideLobby: " + PhotonNetwork.joi);

            //if (PhotonNetwork.insideLobby)
                
            if (!PhotonNetwork.connected)
            {
                buttonPlay.SetActive(false);
                //    PhotonNetwork.autoJoinLobby = true;
                PhotonNetwork.ConnectUsingSettings("0.0.1");
            } else if (!PhotonNetwork.insideLobby)
            {
                buttonPlay.SetActive(false);
                PhotonNetwork.JoinLobby();
            } else
            {
                buttonPlay.SetActive(true);
            }
        }

        public virtual void OnLeftLobby()
        {
            buttonPlay.SetActive(false);
            Debug.Log("OnLeftLobby");
        }

        public virtual void OnLeftRoom()
        {
            buttonPlay.SetActive(false);
            Debug.Log("OnLeftRoom");
        }

        public virtual void OnConnectedToPhoton()
        {
            Debug.Log("OnConnectedToPhoton");
        }

        public virtual void OnConnectedToMaster()
        {
            Debug.Log("OnConnectedToMaster");
            buttonPlay.SetActive(true);
        }

        public virtual void OnJoinedLobby()
        {
            Debug.Log("OnJoinedLobby");
            buttonPlay.SetActive(true);
        }

        public virtual void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            Debug.LogError("OnFailedToConnectToPhoton: " + cause);
            buttonPlay.SetActive(false);
            Connect();
        }

        public void OnConnectionFail()
        {
            Debug.Log("OnConnectionFail");
            buttonPlay.SetActive(false);
            Connect();
        }


    }
}
