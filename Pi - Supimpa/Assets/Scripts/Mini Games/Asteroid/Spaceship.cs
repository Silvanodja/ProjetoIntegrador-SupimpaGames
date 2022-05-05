using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;


public class Spaceship : MonoBehaviour
{
    Rigidbody2D rb;
    public float movementSpeed = 5f;
    public float rotationSpeed = 160f;

    float rotation;

    public ObjectPool bulletPool;
    public Restarter restarter;
    public Health damage;
    public RectTransform Rtransform;
    public GameObject left, right;

    public Camera cam;

    Vector3 mousePosition;
    Vector2 movement;
    float angle;
    Vector3 dir;

    void Start()
    {
        
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float v = Input.GetAxisRaw("Vertical");

        rb.AddForce(((Vector2)transform.up * v * movementSpeed) - rb.velocity, ForceMode2D.Force);

        //rb.MovePosition(rb.position + movement * movementSpeed * Time.fixedDeltaTime);

       // Vector2 lookDirection = mousePosition - rb.position;
        //float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg - 90f;
        //rb.rotation = angle;

        //Rtransform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle * Time.deltaTime));
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
        //movement.x = Input.GetAxisRaw("Horizontal");
        //movement.y = Input.GetAxisRaw("Vertical");

        //mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        //dir =  Rtransform.position - mousePosition;
        //dir.Normalize();
        //angle = Mathf.Rad2Deg * (Mathf.Atan2(dir.x, dir.y));


        //Vector3 mousePos = Input.mousePosition;
        //mousePos = Camera.main.ScreenToWorldPoint(mousePos);

        //Vector2 direction = new Vector2(mousePos.x - transform.position.x, mousePos.y - transform.position.y);

        //transform.up = direction;


        float rotationDir = 0f;
        if (Input.GetKey(KeyCode.A))
        {
            rotationDir = 1f;
        }
        else if(Input.GetKey(KeyCode.D))
        {
            rotationDir = -1f;
        }

        rotation += rotationDir * Time.smoothDeltaTime * rotationSpeed;

        transform.localEulerAngles = new Vector3(0, 0, rotation);

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
