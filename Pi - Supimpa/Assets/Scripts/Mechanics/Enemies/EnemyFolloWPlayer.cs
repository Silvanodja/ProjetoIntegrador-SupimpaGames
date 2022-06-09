using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFolloWPlayer : MonoBehaviour
{
    public float speed;
    Transform player;
    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
    }
}
