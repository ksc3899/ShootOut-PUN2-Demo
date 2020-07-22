using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class GameSetup : MonoBehaviour
{
    public Transform[] instantiationPositions;
    public Transform[] cameraPositions;

    private IEnumerator Start()
    {
        Debug.Log("Creating Player");
        yield return new WaitForSeconds(2f);
        int randomNumber = Random.Range(0, instantiationPositions.Length);
        Camera.main.transform.position = cameraPositions[randomNumber].position;
        Camera.main.transform.localRotation = cameraPositions[randomNumber].localRotation;
        PhotonNetwork.Instantiate("Player", instantiationPositions[randomNumber].position, Quaternion.identity); 
    }
}
