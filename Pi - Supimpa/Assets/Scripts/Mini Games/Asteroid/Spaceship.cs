using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Spaceship : MonoBehaviour
{
    public float movementSpeed = 5f;
    Rigidbody2D rb;
    public Rigidbody2D torreta;
    Vector2 mousePosition;
    Vector2 movement;

    public ObjectPool bulletPool;
    public Health damage;
    Vector3 posNaTela;
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);
        torreta.MovePosition(rb.position);
        Vector2 lookDir = mousePosition - torreta.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        torreta.rotation = angle;
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
        Camera.main.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
    }
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        

        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var bullet = bulletPool.GetInstance();
            bullet.transform.position = torreta.transform.position;
            bullet.transform.rotation = torreta.transform.rotation;

            bullet.SetActive(true);
        }

        if (damage.health == 0)
        {
            gameObject.SetActive(false);
        }

        //posNaTela = Camera.main.WorldToViewportPoint(transform.position);

        //if (posNaTela.x < 0)
        //{
        //    posNaTela.x = 1;
        //}
        //else if (posNaTela.x > 1)
        //{
        //    posNaTela.x = 0;
        //}

        //if (posNaTela.y < 0)
        //{
        //    posNaTela.y = 1;
        //}
        //else if (posNaTela.y > 1)
        //{
        //    posNaTela.y = 0;
        //}
        //transform.position = Camera.main.ViewportToWorldPoint(posNaTela);

    }
}
