using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    Rigidbody2D rb;

    public float movementSpeed = 50f;
    public float rotationSpeed = 200f;

    float rotationDir;
    float rotation;
    
    Vector2 direction;
    Vector3 posNaTela;
    Vector3 extremidades;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rotationDir = Random.value > 0.5f ? -1f : 1f;
        direction = (Camera.main.transform.position - transform.position).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = movementSpeed * direction;
        posNaTela = Camera.main.WorldToViewportPoint(transform.position);
        if (posNaTela.x < 0)
        {
            posNaTela.x = 1;
        }
        else if (posNaTela.x > 1)
        {
            posNaTela.x = 0;
        }

        if (posNaTela.y < 0)
        {
            posNaTela.y = 1;
        }
        if (posNaTela.y > 1)
        {
            posNaTela.y = 0;
        }
        transform.position = new Vector3(Camera.main.ViewportToWorldPoint(posNaTela).x, Camera.main.ViewportToWorldPoint(posNaTela).y, 0);        
    }

    void Update()
    {
        rotation += rotationDir * Time.smoothDeltaTime * rotationSpeed;

        extremidades = Camera.main.WorldToViewportPoint(transform.position);
    }
    private void OnEnable()
    {
        Vector3 spawn = new Vector3(Random.Range(extremidades.x = 0, extremidades.x = 1), 0.9f, 0);
        transform.position = Camera.main.ViewportToWorldPoint(spawn);
    }
}
