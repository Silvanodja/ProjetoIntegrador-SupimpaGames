using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SpawnManager : MonoBehaviour
{
    public GameObject playerPrefab, nave, miniGame, playerDetect;

    public Transform spawnPoint;
    public Camera cam;
    public GameObject gm;

    bool ativado = false;

    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {

        pos.z = -10;
        gm = PhotonNetwork.Instantiate(playerPrefab.name, spawnPoint.position, Quaternion.identity);
        playerDetect.transform.SetParent(gm.transform);
        playerDetect.transform.position = gm.transform.position;
        //cam.transform.SetParent(gm.transform);
        //gm.transform.SetParent(nave.transform);
    }
    private void Update()
    {
        if (miniGame.GetComponent<Restarter>().ativado == false)
        {
            Camera.main.transform.position = new Vector3(gm.transform.position.x, gm.transform.position.y, -10);
            ativado = true;
        }
        else if (miniGame.GetComponent<Restarter>().ativado == true && ativado == true)
        {
            Camera.main.transform.position = new Vector3(miniGame.GetComponent<Restarter>().nave.transform.position.x, miniGame.GetComponent<Restarter>().nave.transform.position.y, -10);
            ativado = false;
        }
    }
}
