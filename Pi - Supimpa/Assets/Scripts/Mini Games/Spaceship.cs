using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Spaceship : MonoBehaviour
{
    public Rigidbody2D rb;
    public float movementSpeed = 5f;

    public GameObject bulletPrefab;
    public Restarter restarter;
    public Health damage;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Vector3 Position = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0);
        rb.velocity = Position * movementSpeed;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Asteroid")
        {
            Destroy(other.gameObject);
            damage.health = damage.health - 1;
        }
    }

    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        transform.up = direction;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
        }

        if (damage.health == 0)
        {
            restarter.Restart();
            gameObject.SetActive(false);
        }
    }
}
