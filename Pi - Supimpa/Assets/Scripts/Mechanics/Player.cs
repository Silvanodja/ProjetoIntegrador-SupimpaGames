using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : MonoBehaviour
{
    public float speed = 10;
    Rigidbody2D physics;
    public TextMeshProUGUI locationText;
    Animator anim;

    void Start()
    {
        physics = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));


        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        physics.velocity = new Vector2(speed * horizontal, speed * vertical);

        if (horizontal > 0 || horizontal < 0)
        {
            anim.SetBool("Side", true);
        }
        else if (vertical > 0 || vertical < 0)
        {
            anim.SetBool("Side", false);
        }

        if (horizontal < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (horizontal > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
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
