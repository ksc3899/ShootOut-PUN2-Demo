using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerShooting : MonoBehaviourPun
{
    public ParticleSystem muzzleFlash;
    public ParticleSystem shell;
    public Transform bulletSpawnParent;

    private GameObject bullet;

    private void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (Input.GetMouseButtonDown(0))
        {
            GetComponent<PhotonView>().RPC("PlayShootEffect", RpcTarget.AllBuffered);
            bullet = PhotonNetwork.Instantiate("Bullet", bulletSpawnParent.position, this.transform.localRotation);
        }
    }

    [PunRPC]
    public void PlayShootEffect()
    {
        this.muzzleFlash.Play();
        this.shell.Play();
    }
}
