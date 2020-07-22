using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using System;
using TMPro;
using UnityEngine;

public class LobbyController : MonoBehaviourPunCallbacks
{
    public GameObject joinLobbyButton;
    public GameObject lobbyPanel;
    public GameObject mainPanel;
    public TMP_InputField playerNameInput;
    public Transform roomsContainer;
    public GameObject roomListingPrefab;

    private string roomName;
    private int roomSize;
    private List<RoomInfo> roomListings;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        joinLobbyButton.SetActive(true);
        roomListings = new List<RoomInfo>();

        string defaultName = "Player " + UnityEngine.Random.Range(0, 1000).ToString();
        PhotonNetwork.NickName = PlayerPrefs.GetString("Player Name", defaultName);
        playerNameInput.text = PhotonNetwork.NickName;
    }

    public void PlayerNameUpdate(string nameInput)
    {
        PhotonNetwork.NickName = nameInput;
        PlayerPrefs.SetString("Player Name", nameInput);
    }

    public void JoinLobby()
    {
        lobbyPanel.SetActive(true);
        mainPanel.SetActive(false);
        PhotonNetwork.JoinLobby();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        int i;
        foreach (RoomInfo room in roomList)
        {
            if (room.PlayerCount > 0)
            {
                roomListings.Add(room);
                ListRoom(room);
            }
            
        }
    }
    
    private void ListRoom(RoomInfo room)
    {
        if(room.IsOpen && room.IsVisible)
        {
            GameObject tempListing = Instantiate(roomListingPrefab, roomsContainer);
            RoomButton tempButton = tempListing.GetComponent<RoomButton>();
            tempButton.SetRoom(room.Name, room.MaxPlayers, room.PlayerCount);
        }
    }

    public void OnRoomNameChanged(string nameIn)
    {
        roomName = nameIn;
    }

    public void OnRoomSizeChanged(string sizeIn)
    {
        roomSize = int.Parse(sizeIn);
    }

    public void CreateRoom()
    {
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = (byte)roomSize };
        PhotonNetwork.CreateRoom(roomName, roomOptions);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError("Failed to create room. Check if room name is unique.");
    }

    public void CancelMatchmaking()
    {
        mainPanel.SetActive(true);
        lobbyPanel.SetActive(false);
        PhotonNetwork.LeaveLobby();
    }
}

    
