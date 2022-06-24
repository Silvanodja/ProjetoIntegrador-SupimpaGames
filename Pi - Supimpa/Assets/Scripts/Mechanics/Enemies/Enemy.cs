using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject[] players;
    public float speed;
    int nearTransform;

    private void Start()
    {
        
    }

    private void OnEnable()
    {
        
        players = GameObject.FindGameObjectsWithTag("Player");
    }
    private void Update()
    {
        //float distaceOne = Vector2.Distance(transform.position, players[0].transform.position);
        //float distaceTwo = Vector2.Distance(transform.position, players[1].transform.position);

        //if(distaceOne < distaceTwo)
        //{
        //    nearstPlayer = players[0];
        //}
        //else
        //{
        //    nearstPlayer = players[1];
        //}

        if (players != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, ClosestPlayer().position, speed * Time.deltaTime);
        }
    }

    Transform ClosestPlayer()
    {
        nearTransform = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (Vector3.Distance(players[i].transform.position, transform.position) < Vector3.Distance(players[nearTransform].transform.position, transform.position))
            {
                nearTransform = i;
            }
        }

        //Numa array de transforms, o mais perto será o index "maisPertoTransform"
        return players[nearTransform].transform;
    }

}
