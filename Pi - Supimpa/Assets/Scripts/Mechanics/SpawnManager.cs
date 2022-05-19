using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
   public GameObject playerPrefab;

    public Transform spawnPoint;
    public Camera cam;
    GameObject gm;
Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos.z = -10;
        gm = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity);
        cam.transform.SetParent(gm.transform);
        cam.transform.position = new Vector3(gm.transform.position.x, gm.transform.position.y, -10);        
    }
}
