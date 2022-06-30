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
    public CameraController gameCamera;
    public MiniGameManager miniGameManager;
    public bool hasWeapon;

    Weapon gun;

    private void Start()
    {
        gun = FindObjectOfType<Weapon>();
        gameCamera = FindObjectOfType<CameraController>();
        miniGameManager = FindObjectOfType<MiniGameManager>();
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
                    if (FindObjectOfType<AudioManager>().isPlaying("Invasion"))
                    {
                        FindObjectOfType<AudioManager>().Pause("Invasion");
                    }
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
        if (hit.gameObject.GetComponent<Interactable>() != null)
        {
            if (!hasWeapon)
            {
                hit.gameObject.GetComponent<Interactable>().miniGame.SetActive(true);
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    bool Asteroids()
    {
        Collider2D hit = InteractionResult();
        if (hit.gameObject.GetComponent<Interact>() != null)
        {
            if (!hasWeapon)
            {
                hit.gameObject.GetComponent<Interact>().begin = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
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
