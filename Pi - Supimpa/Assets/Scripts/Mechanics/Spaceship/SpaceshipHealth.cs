using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class SpaceshipHealth : MonoBehaviourPunCallbacks
{
    public Slider healthSlider;
    public Slider oxygenSlider;
    public GameObject canvas;
    public int maxHealth = 100;
    public float maxOxygen = 100;
    public int currentHealth;
    public float currentOxygen;

    public bool tookDamage;
    public bool restoredOxygen;

    float timer;
    int seconds;

    bool halfHealth;
    bool lowHealth;
    bool lowOxygen;

    public GameObject defeat;
    public Notification notification;
    public GameObject notificationHalf;
    public GameObject notificationLow;
    public GameObject notificationOLow;

    void Start()
    {
        currentHealth = maxHealth;
        currentOxygen = maxOxygen;
        halfHealth = false;
        lowHealth = false;
        lowOxygen = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        seconds = (int)timer;

        if (seconds == 1)
        {
            oxygenSlider.value--;
            timer = 0;
        }

        if (currentHealth <= 0 || oxygenSlider.value <= 0)
        {
            defeat.SetActive(true);
            photonView.RPC(nameof(Defeat), RpcTarget.All);
        }

        if (currentHealth == 50 && !halfHealth)
        {
            notification.notificationPrefab = notificationHalf;
            notification.NotificationInput();
            halfHealth = true;
        }

        if (currentHealth == 20 && !lowHealth)
        {
            notification.notificationPrefab = notificationLow;
            notification.NotificationInput();
            lowHealth = true;
        }

        if (oxygenSlider.value <= 30 && !lowOxygen)
        {
            notification.notificationPrefab = notificationOLow;
            notification.NotificationInput();
            lowOxygen = true;
        }
    }

    public void DamageHealth(int damage)
    {
        tookDamage = false;
        photonView.RPC(nameof(TakeDamage), RpcTarget.AllBuffered, damage);
    }

    public void RestoreOxygen(float oxygen)
    {
        lowOxygen = false;
        photonView.RPC(nameof(SetOxygen), RpcTarget.AllBuffered, oxygen);
    }

    public void SetMaxHealth(int health)
    {
        healthSlider.maxValue = health;
        healthSlider.value = health;
    }

    public void SetHealth(int health)
    {
        healthSlider.value = health;
    }

    public void SetMaxOxygen(int health)
    {
        oxygenSlider.maxValue = health;
        oxygenSlider.value = health;
    }

    [PunRPC]
    public void SetOxygen(float health)
    {
        oxygenSlider.value += health;
        if (oxygenSlider.value > oxygenSlider.maxValue)
        {
            oxygenSlider.value = oxygenSlider.maxValue;
        }
    }

    [PunRPC]
    public void TakeDamage(int damage)
    {
        if (currentHealth > 0)
        {
            currentHealth -= damage;
            SetHealth(currentHealth);
        }
    }

    [PunRPC]
    public void Defeat()
    {
        Time.timeScale = 0;
    }
}
