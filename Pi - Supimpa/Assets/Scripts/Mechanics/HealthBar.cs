using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class HealthBar : MonoBehaviourPunCallbacks
{
    public Slider slider;
    public GameObject canvas;
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] PlayerController player;

    PhotonView view;

    private void Start()
    {
        view = GetComponent<PhotonView>();
        currentHealth = maxHealth;

    }
    private void Update()
    {
        if (view.IsMine)
        {
            canvas.SetActive(true);
        }
    }

    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = health;
    }

    [PunRPC]
    public void Revive()
    {
        if (view.IsMine){
            currentHealth = maxHealth;
            SetHealth(currentHealth);
        }
    }

    public void TakeDamage(int damage)
    {
        if(currentHealth > 0)
        {
        currentHealth -= damage;
        SetHealth(currentHealth);
        }
        else
        {
            player.Die();
        }
    }
}
