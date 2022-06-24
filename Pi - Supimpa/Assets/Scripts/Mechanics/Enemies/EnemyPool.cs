using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemyPool : MonoBehaviourPunCallbacks
{
    [SerializeField] private string enemyPrefabLocation;
    [SerializeField] private Transform[] enemySpawns;

    [SerializeField] private int beginsInstantiated;
    public GameObject pasta;

    public List<GameObject> listOfObjects;



    void Start()
    {
    }
    private void OnEnable()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("CreateEnemy", RpcTarget.MasterClient);
        }
    }


    [PunRPC]
    private void CreateEnemy()
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            return;
        }
        listOfObjects = new List<GameObject>();

        for (int i = 0; i < beginsInstantiated; i++)
        {
            var enemyObj = PhotonNetwork.InstantiateRoomObject(enemyPrefabLocation, enemySpawns[Random.Range(0, enemySpawns.Length)].position, Quaternion.identity);
            //enemyObj.SetActive(false);
            enemyObj.transform.SetParent(pasta.transform);
        }
    }
}
