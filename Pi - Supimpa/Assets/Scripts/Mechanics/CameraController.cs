using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;
    /*public float offsetSmoothing;
    private Vector3 playerPosition;*/

    void Update()
    {
        /*playerPosition = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, playerPosition, offsetSmoothing * Time.deltaTime);*/
        //transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
    }
}
