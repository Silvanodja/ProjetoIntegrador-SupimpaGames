using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed = 10;
    Rigidbody2D physics;
    public TextMeshProUGUI locationText;

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.tag)
        {
            case "Location":
                if (!locationText.gameObject.activeInHierarchy)
                {
                    locationText.enabled = true;
                }
                locationText.text = collision.GetComponent<Locations>().locationName;
                break;
            default:
                break;
        }
    }
}
