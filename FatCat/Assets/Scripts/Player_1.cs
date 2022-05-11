using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_1 : MonoBehaviour
{
    public float movementSpeed = 5f;
    

    float movementX;
    float movementY;
    Rigidbody2D rb;
    bool cooldown = false;

    public KeyCode cima;
    public KeyCode esquerda;
    public KeyCode direita;
    public KeyCode baixo;

    

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        movementX = 0;
        movementY = 0;
    }
    public void StartCooldown() 
    {
        cooldown = true;
        Invoke("StopCooldown", 0.5f);
    }

    void StopCooldown()
    {
        cooldown = false;
    }


        // Update is called once per frame
    void Update()
    {
        if(cooldown == false)
        {
        rb.velocity = new Vector2(movementX * movementSpeed * Time.deltaTime, movementY * movementSpeed * Time.deltaTime);
        }


        if (Input.GetKeyDown(cima))
        {
            movementY = 1;
        }

        if (Input.GetKeyDown(esquerda))
        {
            movementX = -1;
        }

        if (Input.GetKeyDown(direita))
        {
            movementX= 1;
        }

        if (Input.GetKeyDown(baixo))
        {
            movementY = -1;
        }

        if(Input.GetKeyUp(cima) || Input.GetKeyUp(baixo))
        {
            movementY = 0;
        }

        if (Input.GetKeyUp(esquerda) || Input.GetKeyUp(direita))
        {
            movementX = 0;
        }
    }

   
}
