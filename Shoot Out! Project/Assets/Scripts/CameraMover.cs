using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMover : MonoBehaviour
{
    public GameObject player;
    
    private Vector3 offset;
    private float smoothing = 5f;

    private void Start()
    {
        offset = this.transform.position - player.transform.position;
    }

    private void LateUpdate()
    {
        Vector3 newPosition = offset + player.transform.position;
        this.transform.position = Vector3.Lerp(this.transform.position, newPosition, smoothing * Time.deltaTime);
    }
}
