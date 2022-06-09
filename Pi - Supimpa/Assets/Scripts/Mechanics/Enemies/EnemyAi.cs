using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyAi : MonoBehaviour
{
    Transform target;

    public float speed = 200f;
    public float nextWaipointDistance = 3;

    public float lineOfSite;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    public Animator anim;

    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
       // anim = GameObject.Find("Enemy/Circle").GetComponent<Animator>();
        InvokeRepeating("UpdatePath", 0f, .5f);
    }

    private void UpdatePath()
    {
        foreach (GameObject item in GameObject.FindGameObjectsWithTag("Player"))
        {
            target = item.transform;
            float distanceFromPlayer = Vector2.Distance(target.position, transform.position);
            if (distanceFromPlayer < lineOfSite)
            {
                if (seeker.IsDone())
                {
                    seeker.StartPath(rb.position, target.position, OnPathComplete);
                }
                anim.SetBool("Walking", true);
            }
            else
            {
                anim.SetBool("Walking", false);
            }
        }
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

    void FixedUpdate()
    {
        if (path == null)
        {
            return;
        }
        if (currentWaypoint >= path.vectorPath.Count)
        {
            reachedEndOfPath = true;
            return;
        }
        else
        {
            reachedEndOfPath = false;
        }

        Vector2 direcition = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direcition * speed * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaipointDistance)
        {
            currentWaypoint++;
        }

        

        if (direcition.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (direcition.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
