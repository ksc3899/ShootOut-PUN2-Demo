using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class WaitSceneManager : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI timer;
    public int maximumTime;

    private int currentTime;
    private PhotonView photonView;
    
    private void Start()
    {
        photonView = GetComponent<PhotonView>();
        currentTime = maximumTime;
        timer.text = "00:" + currentTime.ToString();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        photonView.RPC("CoroutineCaller", RpcTarget.All);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        StopAllCoroutines();
        currentTime = maximumTime;
        timer.text = "00:" + currentTime.ToString();
    }

    [PunRPC]
    private void CoroutineCaller()
    {
        StartCoroutine("StartTimer");
    }

    private IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            currentTime -= 1;
            timer.text = "00:" + currentTime.ToString();

            if (currentTime == 0)
                StartGame();
        }
    }

    private void StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Game");
        }
    }
}
