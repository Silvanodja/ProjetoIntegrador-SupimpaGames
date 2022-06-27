using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class PlayerController : MonoBehaviourPunCallbacks //, IPunObservable
{
    public float speed = 10;
    public Transform shootPosition;
    int shootPositionValue = 0;
    Rigidbody2D physics;
    Animator anim;
    InteractionSystem interaction;
    GameObject shot;
    private PhotonView view;

    float horizontal;
    float vertical;

    Pooling shotPooling;

    private Photon.Realtime.Player photonPlayer;
    private int playerId;

    public GameObject life;
    bool cooldown = false;
    [SerializeField] private HealthBar health;

    bool isDeath = false;

    void Start()
    {
        view = GetComponent<PhotonView>();
        physics = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        interaction = GetComponent<InteractionSystem>();
        shotPooling = FindObjectOfType<Pooling>();
    }

    public void StartCooldown()
    {
        cooldown = true;
        Invoke("StopCooldown", 5);
    }

    void StopCooldown()
    {
        cooldown = false;
    }

    [PunRPC]
    public void Initialize(Photon.Realtime.Player player)
    {
        photonPlayer = player;
        playerId = player.ActorNumber;
        GameManagerScript.Instance.Players.Add(this);

        if (!photonView.IsMine)
        {
            physics.isKinematic = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (view.IsMine)
        {
            if (collision.tag == "Alien" && cooldown == false)
            {
                print("Acertou");
                health.TakeDamage(5);
            }
        }
    }

    public void Die()
    {
        if (!isDeath)
        {
            anim.SetTrigger("Death");
        }
        if (view.IsMine)
        {
            isDeath = true;
        }
        photonView.RPC("EnemyForgetPlayer", RpcTarget.MasterClient);
    }

    [PunRPC]
    private void EnemyForgetPlayer()
    {
        if (view.IsMine)
        {
            interaction.hasWeapon = false;
        }
        gameObject.tag = "Player is Death";
    }

    void Update()
    {
        if (view.IsMine)
        {
            life.SetActive(true);
            if (!isDeath)
            {
                horizontal = Input.GetAxisRaw("Horizontal");
                vertical = Input.GetAxisRaw("Vertical");
            }
            else
            {
                horizontal = 0;
                vertical = 0;
            }
            physics.velocity = new Vector2(speed * horizontal, speed * vertical);

            Camera.main.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10);
        }

        if (interaction.hasWeapon)
        {
            anim.SetBool("Gun", true);
        }
        else
        {
            anim.SetBool("Gun", false);
        }

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
            shootPositionValue = 0;
        }
        else if (horizontal == 0)
        {
            anim.SetBool("Side", false);
        }

        if (vertical > 0 && horizontal == 0)
        {
            anim.SetBool("Back", true);
            shootPositionValue = 1;
        }
        else if (vertical == 0 || horizontal != 0)
        {
            anim.SetBool("Back", false);
        }

        if (vertical < 0 && horizontal == 0)
        {
            anim.SetBool("Front", true);
            shootPositionValue = 2;
        }
        else if (vertical == 0 || horizontal != 0)
        {
            anim.SetBool("Front", false);
        }
        
        if (Input.GetMouseButtonDown(0) && interaction.hasWeapon)
        {
            photonView.RPC("Shoot", RpcTarget.All, shootPositionValue);
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

    [PunRPC]
    void Shoot(int shootPositionValue)
    {
        if (horizontal != 0)
        {
            shootPositionValue = 0;
        }

        if (vertical > 0 && horizontal == 0)
        {
            shootPositionValue = 1;
        }

        if (vertical < 0 && horizontal == 0)
        {
            shootPositionValue = 2;
        }
        GameObject newShot = shotPooling.GetObject();

        if (newShot != null)
        {
            switch (shootPositionValue)
            {
                case 0:
                    shootPosition.localPosition = new Vector2(0.7f, -0.12f);
                    newShot.transform.position = shootPosition.transform.position;
                    newShot.GetComponent<GunShot>().speedHorizontal = newShot.GetComponent<GunShot>().originalSpeed;
                    if (transform.localScale.x == -1)
                    {
                        newShot.GetComponent<GunShot>().speedHorizontal *= -1;
                    }
                    newShot.GetComponent<GunShot>().speedVertical *= 0;
                    break;
                case 1:
                    shootPosition.localPosition = new Vector2(0, 0.7f);
                    newShot.transform.position = shootPosition.transform.position;
                    newShot.GetComponent<GunShot>().speedVertical = newShot.GetComponent<GunShot>().originalSpeed;
                    newShot.GetComponent<GunShot>().speedHorizontal *= 0;
                    break;
                case 2:
                    shootPosition.localPosition = new Vector2(0, -0.5f);
                    newShot.transform.position = shootPosition.transform.position;
                    newShot.GetComponent<GunShot>().speedVertical = newShot.GetComponent<GunShot>().originalSpeed * -1;
                    newShot.GetComponent<GunShot>().speedHorizontal *= 0;
                    break;

            }

            newShot.SetActive(true);
            FindObjectOfType<AudioManager>().Play("WeaponShot");
        }
    }
    //public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    //{
    //    if (stream.IsWriting)
    //    {
    //        stream.SendNext(anim.GetBool("Front"));
    //        stream.SendNext(anim.GetBool("Back"));
    //        stream.SendNext(anim.GetBool("Side"));
    //        stream.SendNext(anim.GetBool("Walking"));
    //    }
    //    else
    //    {
    //        anim.SetBool("Front", (bool)stream.ReceiveNext());
    //        anim.SetBool("Back", (bool)stream.ReceiveNext());
    //        anim.SetBool("Side", (bool)stream.ReceiveNext());
    //        anim.SetBool("Walking", (bool)stream.ReceiveNext());
    //    }
    //}
}
