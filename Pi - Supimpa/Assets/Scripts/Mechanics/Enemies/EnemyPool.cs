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

    public List<GameObject> listOfObjects = new();
    public int alienCount;


    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("CreateEnemy", RpcTarget.MasterClient);
        }
        alienCount = beginsInstantiated;

    }

    private void OnEnable()
    {
    }

    private void StopTheme()
    {
        FindObjectOfType<AudioManager>().Stop("Invasion");
        FindObjectOfType<AudioManager>().Play("MainTheme");
    }

    private void Update()
    {
        if (alienCount <= 0)
        {
            alienCount = beginsInstantiated;
            StopTheme();
        }
    }

    [PunRPC]
    public void RespawnEnemies()
    {
        FindObjectOfType<AudioManager>().Pause("MainTheme");
        FindObjectOfType<AudioManager>().Play("Invasion");
        foreach (var item in listOfObjects)
        {
            alienCount = beginsInstantiated;
            item.SetActive(true);
            item.GetComponent<Collider2D>().enabled = true;
        }
    }

    [PunRPC]
    private void CreateEnemy()
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            return;
        }

        for (int i = 0; i < beginsInstantiated; i++)
        {
            var enemyObj = PhotonNetwork.InstantiateRoomObject(enemyPrefabLocation, enemySpawns[Random.Range(0, enemySpawns.Length)].position, Quaternion.identity);
            listOfObjects.Add(enemyObj);
            enemyObj.transform.SetParent(pasta.transform);
            listOfObjects[i].SetActive(false);
        }
        photonView.RPC("EnemyDisactive", RpcTarget.Others);
    }

    [PunRPC]
    private void EnemyDisactive()
    {
        if (PhotonNetwork.IsMasterClient == false)
        {
            foreach (var item in GameObject.FindObjectsOfType<EnemyAi>())
            {
                listOfObjects.Add(item.gameObject);
                item.gameObject.SetActive(false);
            }
        }
    }
}
