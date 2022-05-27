using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
public class Player : MonoBehaviour, IPunObservable
{
    public float speed = 10;
    Rigidbody2D physics;
    //public TextMeshProUGUI locationText;
    Animator anim;

    private PhotonView view;

    float horizontal;
    float vertical;

    void Start()
    {
        view = GetComponent<PhotonView>();
        physics = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Debug.Log(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (view.IsMine)
        {
            horizontal = Input.GetAxisRaw("Horizontal");
            vertical = Input.GetAxisRaw("Vertical");

            physics.velocity = new Vector2(speed * horizontal, speed * vertical);

            if (horizontal != 0 || vertical != 0)
            {
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }

            if (horizontal != 0)
            {
                anim.SetBool("Side", true);
            }
            else if (horizontal == 0)
            {
                anim.SetBool("Side", false);
            }

            if (vertical > 0 && horizontal == 0)
            {
                anim.SetBool("Back", true);
            }
            else if (vertical == 0 || horizontal != 0)
            {
                anim.SetBool("Back", false);
            }

            if (vertical < 0 && horizontal == 0)
            {
                anim.SetBool("Front", true);
            }
            else if (vertical == 0 || horizontal != 0)
            {
                anim.SetBool("Front", false);
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
    }
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(anim.GetBool("Front"));
            stream.SendNext(anim.GetBool("Back"));
            stream.SendNext(anim.GetBool("Side"));
            stream.SendNext(anim.GetBool("Walking"));
        }
        else
        {
            anim.SetBool("Front", (bool)stream.ReceiveNext());
            anim.SetBool("Back", (bool)stream.ReceiveNext());
            anim.SetBool("Side", (bool)stream.ReceiveNext());
            anim.SetBool("Walking", (bool)stream.ReceiveNext());
        }
    }
}
