using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Spaceship : MonoBehaviour
{
    public float movementSpeed = 5f;
    Rigidbody2D rb;
    Vector2 mousePosition;
    Vector2 movement;

    public ObjectPool bulletPool;
    public Health damage;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }

    void OnCollisionEnter2D(Collision2D other)
    {

        if (other.gameObject.tag == "Asteroid")
        {
            other.gameObject.SetActive(false);

            damage.health = damage.health - 1;
        }
    }


    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var bullet = bulletPool.GetInstance();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.SetActive(true);
        }

        if (damage.health == 0)
        {
            gameObject.SetActive(false);
        }
    }
}
