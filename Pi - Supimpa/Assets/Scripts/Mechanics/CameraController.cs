using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class CameraController : MonoBehaviour
{
    public GameObject player;
    public bool miniGameIsPlaying = false;
    private PhotonView viewCamera;

    void Start()
    {
        viewCamera = GetComponent<PhotonView>();

        //if (!viewCamera.IsMine)
        //{
        //    gameObject.SetActive(false);
        //}
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.M))
        //{
        //    transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        //}
    }
}
