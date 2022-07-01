using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviourPunCallbacks
{
    public Slider slider;
    public GameObject canvas;
    public int maxHealth = 100;
    public int currentHealth;
    [SerializeField] EnemyAi enemy;


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
        currentHealth = maxHealth;
        SetHealth(currentHealth);
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            SetHealth(currentHealth);
        }
        else
        {
            enemy.Die();
        }
    }
}
