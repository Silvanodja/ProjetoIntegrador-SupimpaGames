using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class InteractionSystem : MonoBehaviourPunCallbacks
{
    public Transform detectionPoint;
    public float detectionRadius = 1f;
    public LayerMask detectionLayer;
    //public GameObject miniGameAsteroids;
    public CameraController gameCamera;
    //public MiniGameManager miniGameManager;
    public bool hasWeapon;

    Weapon gun;

    private void Start()
    {
        gun = FindObjectOfType<Weapon>();
        gameCamera = FindObjectOfType<CameraController>();
    }

    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                if (MiniGame())
                {
                    gameCamera.miniGameIsPlaying = true;
                    FindObjectOfType<AudioManager>().Play("MiniGameTheme");
                    FindObjectOfType<AudioManager>().Pause("MainTheme");
                }

                else if (Asteroids())
                {
                    gameCamera.miniGameIsPlaying = true;
                }

                else if (WeaponPickUp())
                {
                    if (!hasWeapon && gun.gunCount > 0)
                    {
                        hasWeapon = true;
                        photonView.RPC(nameof(TakeGun), RpcTarget.AllBuffered);
                    }
                    else if (hasWeapon)
                    {
                        hasWeapon = false;
                        photonView.RPC(nameof(ReturnGun), RpcTarget.AllBuffered);
                    }
                }
            }
        }
    }

    [PunRPC]
    public void TakeGun()
    {
        gun.gunCount--;
    }

    [PunRPC]
    public void ReturnGun()
    {
        gun.gunCount++;
    }

    bool MiniGame()
    {
        Collider2D hit = InteractionResult();
        //Debug.Log(hit.gameObject.GetComponent<Interactable>());
        if (hit.gameObject.GetComponent<Interactable>() != null)
        {
            hit.gameObject.GetComponent<Interactable>().miniGame.SetActive(true);
            return true;
        }
        else
        {
            //hit.gameObject.SetActive(false);
            return false;
        }
    }

    bool Asteroids()
    {
        Collider2D hit = InteractionResult();
        //Debug.Log(hit.gameObject.GetComponent<Interactable>());
        if (hit.gameObject.GetComponent<Interact>() != null)
        {
            hit.gameObject.GetComponent<Interact>().begin = true;
            return true;
        }
        else
        {
            //hit.gameObject.SetActive(false);
            return false;
        }
    }

    bool WeaponPickUp()
    {
        Collider2D hit = InteractionResult();

        if (hit.gameObject.GetComponent<Weapon>() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public Collider2D InteractionResult()
    {
        return Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        return Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(detectionPoint.position, detectionRadius);
    }
}
