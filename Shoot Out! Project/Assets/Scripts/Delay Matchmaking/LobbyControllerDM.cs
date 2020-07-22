using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyControllerDM : MonoBehaviourPunCallbacks
{
    public GameObject loadingButton;
    public GameObject startButton;
    public GameObject cancelbutton;
    public int roomSize;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        loadingButton.SetActive(false);
        startButton.SetActive(true);
    }

    public void StartGame()
    {
        startButton.SetActive(false);
        cancelbutton.SetActive(true);
        Debug.Log("Joining random room...");
        PhotonNetwork.JoinRandomRoom();
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Creating new room...");
        CreateRoom();
    }

    private void CreateRoom()
    {
        string roomName = "Room: " + Random.Range(0, 10000).ToString();
        RoomOptions roomOptions = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("Could not create room...");
        CreateRoom();
    }

    public void CancelGame()
    {
        cancelbutton.SetActive(false);
        startButton.SetActive(true);
        PhotonNetwork.LeaveRoom();
    }
}
