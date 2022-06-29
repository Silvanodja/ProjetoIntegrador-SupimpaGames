using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using Photon.Pun;
public class EnemyAi : MonoBehaviourPunCallbacks
{
    public float speed = 200f;
    public float nextWaipointDistance = 3;

    public float lineOfSite;

    Path path;
    int currentWaypoint = 0;

    Seeker seeker;
    Rigidbody2D rb;

    Animator anim;

    GameObject[] players;
    int nearTransform;

    Transform myTransform;

    [SerializeField] private bool isWalking;

    [SerializeField] private float walkTime;
    float walkCkounter;

    [SerializeField] private float waitTime;
    float waitCounter;

    int walkDirecition;

    public int maxHealth = 100;
    int currentHealth;

    private PhotonView view;
    [SerializeField] private EnemyHealthBar  health;

    private EnemyPool enemyPool;

    void Start()
    {
        view = GetComponent<PhotonView>();
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        myTransform = GetComponent<Transform>();
        currentHealth = maxHealth;
        waitCounter = waitTime;
        walkCkounter = walkTime;
        enemyPool = FindObjectOfType<EnemyPool>();
        ChoseDirection();
    }

    Transform ClosestPlayer()
    {
        nearTransform = 0;
        for (int i = 0; i < players.Length; i++)
        {
            if (Vector3.Distance(players[i].transform.position, transform.position) < Vector3.Distance(players[nearTransform].transform.position, transform.position))
            {
                nearTransform = i;
            }
        }

        return players[nearTransform].transform;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
    }

    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    void ChoseDirection()
    {
        walkDirecition = Random.Range(0, 4);
        isWalking = true;
        walkCkounter = walkTime;
    }

    void FixedUpdate()
    {
        players = GameObject.FindGameObjectsWithTag("Player");

        float distanceFromPlayer = Vector2.Distance(ClosestPlayer().position, transform.position);
        if (distanceFromPlayer < lineOfSite)
        {
            if (seeker.IsDone())
            {
                seeker.StartPath(transform.position, ClosestPlayer().position, OnPathComplete);
                if (ClosestPlayer().position.x > transform.position.x)
                {
                    transform.localScale = new Vector3(-1, 1, 1);
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);
                }
                anim.SetBool("Walking", true);
            }

        }
        else
        {
            if (isWalking)
            {
                anim.SetBool("Walking", true);
                walkCkounter -= Time.deltaTime;

                switch (walkDirecition)
                {
                    case 0:
                        rb.velocity = new Vector2(0, speed);
                        transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 1:
                        rb.velocity = new Vector2(speed, 0);
                        transform.localScale = new Vector3(-1, 1, 1);
                        break;
                    case 2:
                        rb.velocity = new Vector2(0, -speed);
                        transform.localScale = new Vector3(1, 1, 1);
                        break;
                    case 3:
                        rb.velocity = new Vector2(-speed, 0);
                        transform.localScale = new Vector3(1, 1, 1);
                        break;
                }

                if (walkCkounter < 0)
                {
                    isWalking = false;
                    waitCounter = waitTime;
                }
            }
            else
            {
                anim.SetBool("Walking", false);
                waitCounter -= Time.deltaTime;

                rb.velocity = Vector2.zero;

                if (waitCounter < 0)
                {
                    ChoseDirection();
                }
            }
        }

        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, ClosestPlayer().position, speed * Time.deltaTime);

        float distance = Vector2.Distance(transform.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaipointDistance)
        {
            currentWaypoint++;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.tag == "Shoot")
            {
                print("Acertou");
                collision.GetComponent<GunShot>().photonView.RPC("Desactivate", RpcTarget.All);
                health.photonView.RPC("TakeDamage", RpcTarget.All, 10);
            }        
    }

    public void Die()
    {
        photonView.RPC("Desactive", RpcTarget.All);
    }

    [PunRPC]
    public void Desactive()
    {
        enemyPool.alienCount++;
        gameObject.SetActive(false);
    }
}
