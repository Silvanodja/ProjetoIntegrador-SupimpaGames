using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 10;
    Rigidbody2D physics;

    void Start()
    {
        physics = GetComponent<Rigidbody2D>();    
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        physics.velocity = new Vector2(speed * horizontal, speed * vertical);
    }
}
