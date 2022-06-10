using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionSystem : MonoBehaviour
{
    public Transform detectionPoint;
    public float detectionRadius = 1f;
    public LayerMask detectionLayer;
    //public GameObject miniGameAsteroids;
    public CameraController gameCamera;
    public bool hasWeapon;

    Weapon gun;

    private void Start()
    {
        gun = FindObjectOfType<Weapon>();
    }

    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                //Debug.Log(MiniGame());
                if (MiniGame())
                {
                    //Debug.Log("minigame");
                    gameCamera.miniGameIsPlaying = true;
                    FindObjectOfType<AudioManager>().Play("MiniGameTheme");
                    FindObjectOfType<AudioManager>().Pause("MainTheme");
                }

                else if (WeaponPickUp())
                {
                    if (!hasWeapon && gun.gunCount > 0)
                    {
                        hasWeapon = true;
                        gun.gunCount--;
                    }
                    else if (hasWeapon)
                    {
                        hasWeapon = false;
                        gun.gunCount++;
                    }
                }
            }
        }
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
