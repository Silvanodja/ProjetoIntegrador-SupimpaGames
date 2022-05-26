using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceshipWeapon : MonoBehaviour
{
    public float movementSpeed = 5f;
    Rigidbody2D rb;
    Vector2 mousePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    //Vector2 movement;
    private void FixedUpdate()
    {
        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }    

    // Update is called once per frame
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
