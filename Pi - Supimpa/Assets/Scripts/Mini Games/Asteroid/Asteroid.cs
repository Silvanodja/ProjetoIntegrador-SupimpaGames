using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody2D rb;

    public float movementSpeed = 50f;
    public float rotationSpeed = 200f;
   
    float rotationDir;
    float rotation;

    Vector2 direction;

    void Start()
    {
        rotationDir = Random.value > 0.5f ? -1f : 1f;
        direction = (Vector3.zero - transform.position).normalized;
    }

    void FixedUpdate()
    {
        rb.velocity = movementSpeed * direction;

    }

    void Update()
    {
        rotation += rotationDir * Time.smoothDeltaTime * rotationSpeed;
        
    }

    //public Sprite[] sprites;

    //public float size = 1.0f;

    //public float minSize = 0.5f;

    //public float maxSize = 1.5f;

    //private SpriteRenderer _spriteRenderer;

    //private Rigidbody2D _rigidbody;

    //private voide Awake()
    //{
    //    _spriteRenderer = GetComponent<SpriteRenderer>();
    //    _rigidbody = GetComponent < Rigidbody2D>();
    //}

    //private void Start()
    //{
    //    _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

    //    this.transform.eulerAngles = new Vector3(0.0f, 0.0f, Random.value * 360.0f);
    //    this.transform.localScale = Vector3.one * this.size;

    //    _rigidbody.mas = this.size;
    //}
}
