using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomControllerQM : MonoBehaviourPunCallbacks
{
    public override void OnJoinedRoom()
    {
        StartGame();
    }

    private void StartGame()
    {
        if(PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
