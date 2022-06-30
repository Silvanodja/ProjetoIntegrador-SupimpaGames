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
    CameraController miniGame;

    float horizontal;
    float vertical;

    Pooling shotPooling;

    private Photon.Realtime.Player photonPlayer;
    private int playerId;

    public GameObject life;
    bool cooldown = false;
    [SerializeField] private HealthBar health;
    DeathManager death;

    public bool isDeath = false;

    GameObject[] respawnPoints;

    [SerializeField] private float respawnTime;
    float respawnCounter;

    void Start()
    {
        respawnCounter = respawnTime;
        respawnPoints = GameObject.FindGameObjectsWithTag("RespawnPoint");
        view = GetComponent<PhotonView>();
        physics = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        interaction = GetComponent<InteractionSystem>();
        shotPooling = FindObjectOfType<Pooling>();
        miniGame = FindObjectOfType<CameraController>();
        death = FindObjectOfType<DeathManager>();
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
            //physics.isKinematic = true;
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

    public void Revive()
    {
        if (view.IsMine)
        {
            isDeath = false;
            anim.SetTrigger("Revive");
            gameObject.tag = "Player";
        }
        health.photonView.RPC("Revive", RpcTarget.MasterClient);
    }

    public void Die()
    {
        if (!isDeath)
        {
            interaction.hasWeapon = false;
            anim.SetTrigger("Death");
        }
        if (view.IsMine)
        {
            isDeath = true;
            death.DeathCount(1);
        }
        photonView.RPC("EnemyForgetPlayer", RpcTarget.All);
    }

    [PunRPC]
    void EnemyForgetPlayer()
    {
        gameObject.tag = "Player is Death";
    }

    void Update()
    {
        if (isDeath)
        {
            respawnCounter -= Time.deltaTime;
            if (respawnCounter < 0)
            {
                int position = Random.Range(0, respawnPoints.Length);
                gameObject.transform.position = respawnPoints[position].transform.position;
                respawnCounter = respawnTime;
                Revive();
            }
        }
        if (view.IsMine)
        {
            life.SetActive(true);
            if (!isDeath && !miniGame.miniGameIsPlaying)
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
                    newShot.gameObject.transform.localRotation = Quaternion.AngleAxis(90, Vector3.forward);
                    if (transform.localScale.x == -1)
                    {
                        newShot.GetComponent<GunShot>().speedHorizontal *= -1;
                    }
                    newShot.GetComponent<GunShot>().speedVertical *= 0;
                    break;
                case 1:
                    shootPosition.localPosition = new Vector2(0, 0.7f);
                    newShot.transform.position = shootPosition.transform.position;
                    newShot.gameObject.transform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
                    newShot.GetComponent<GunShot>().speedVertical = newShot.GetComponent<GunShot>().originalSpeed;
                    newShot.GetComponent<GunShot>().speedHorizontal *= 0;
                    break;
                case 2:
                    shootPosition.localPosition = new Vector2(0, -0.5f);
                    newShot.transform.position = shootPosition.transform.position;
                    newShot.gameObject.transform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
                    newShot.GetComponent<GunShot>().speedVertical = newShot.GetComponent<GunShot>().originalSpeed * -1;
                    newShot.GetComponent<GunShot>().speedHorizontal *= 0;
                    break;

            }

            newShot.SetActive(true);
            FindObjectOfType<AudioManager>().Play("WeaponShot");
        }
    }
}
