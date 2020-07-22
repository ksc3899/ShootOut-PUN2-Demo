using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class RoomButton : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI sizeText;

    private string roomName;
    /*private int roomSize;
    private int playerCount;*/

    public void JoinRoomOnClick()
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void SetRoom(string nameInput, int sizeInput, int countInput)
    {
        roomName = nameInput;
        /*roomSize = sizeInput;
        playerCount = countInput;*/
        nameText.text = nameInput;
        sizeText.text = countInput + "/" + sizeInput;
    }
}
