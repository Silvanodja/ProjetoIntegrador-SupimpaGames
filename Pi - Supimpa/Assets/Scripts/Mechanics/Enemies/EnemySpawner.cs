using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] spawnPoints;
    public GameObject enemy;
    public float startTimeBtwSpawns;
    float timeBtwSpawns;

    private void Update()
    {
        if(PhotonNetwork.IsMasterClient == false)
        {            
            return;
        }
        if(timeBtwSpawns >= startTimeBtwSpawns)
        {
            Vector3 spawnPosition = spawnPoints[Random.Range(0, spawnPoints.Length)].position;
            PhotonNetwork.Instantiate(enemy.name, spawnPosition, Quaternion.identity);
            timeBtwSpawns = 0;
        }
        else
        {
            timeBtwSpawns += Time.deltaTime;
        }
    }
}
