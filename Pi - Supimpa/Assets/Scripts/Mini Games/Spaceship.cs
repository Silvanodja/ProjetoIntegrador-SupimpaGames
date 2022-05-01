using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Spaceship : MonoBehaviour
{
    Rigidbody2D rb;
    public float movementSpeed = 5f;

    public ObjectPool bulletPool;
    public Restarter restarter;
    public Health damage;

    public GameObject left, right;

    public Camera cam;

    Vector2 mousePosition;
    Vector2 movement;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

        Vector2 lookDirection = mousePosition - rb.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
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
    private void OnEnable()
    {
        
        gameObject.transform.position = new Vector3(0, 0, 0);
    }

    private void OnDisable()
    {
       
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);

        //Vector3 mousePos = Input.mousePosition;
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        //transform.up = direction;

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var bullet = bulletPool.GetInstance();
            bullet.transform.position = transform.position;
            bullet.transform.rotation = transform.rotation;

            bullet.SetActive(true);
        }

        if (damage.health == 0)
        {
            //restarter.Restart();
            gameObject.SetActive(false);
        }
    }
}
