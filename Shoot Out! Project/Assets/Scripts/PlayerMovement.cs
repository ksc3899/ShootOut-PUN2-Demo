using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerMovement : MonoBehaviourPun
{
    public float moveVelocity = 5f;
    public LayerMask floorMask;

    private float moveHorizontal;
    private float moveVertical;
    private Rigidbody playerRigidbody;
    private Animator animator;
    
    private void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        Move(moveHorizontal, moveVertical);
        TurnAround();
        AnimatePlayer(moveHorizontal, moveVertical);
    }

    private void Move(float moveHorizontal, float moveVertical)
    {
        transform.Translate(Vector3.forward * moveVertical * moveVelocity * Time.deltaTime);
        transform.Translate(Vector3.right * moveHorizontal * moveVelocity * Time.deltaTime);
    }

    private void TurnAround()
    {
        Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit floorHit;

        if (Physics.Raycast(camRay, out floorHit, 100f, floorMask))
        {
            Vector3 playerToFocussedPoint = floorHit.point - this.transform.position;
            playerToFocussedPoint.y = 0f;
            Quaternion newRotation = Quaternion.LookRotation(playerToFocussedPoint);
            playerRigidbody.MoveRotation(newRotation);
        }
    }
    private void AnimatePlayer(float moveHorizontal, float moveVertical)
    {
        bool walking = (moveHorizontal != 0f) || (moveVertical != 0f);

        animator.SetBool("Run", walking);
    }

}
