using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class CycleManager : MonoBehaviourPunCallbacks
{
    int numberOfCycles;
    bool cooldown;
    bool alienCooldown;
    bool asteroidCooldown;
    bool qteCooldown;
    bool alienQTECooldown;
    bool keycodeCooldown;
    [SerializeField]bool rpcCooldown;
    public float floatToInt;
    public float timer;
    float _ktimer;
    float _atimer;
    float _qtimer;
    float _aitimer;
    float _itimer;
    float _rtimer;
    public int keyCodeTimer;
    public int asteroidTimer;
    public int qteTimer;
    public int alienQteTimer;
    public int invasionTimer;
    public int robotTimer;
    public bool robotOver;
    public TMP_Text cycleUI;
    public int keyCodeLenght;
    public float qTEBarTime;
    public int simonSaysLenght;

    public MiniGameManager miniGames;
    public GameObject pool;

    SpaceshipHealth spaceshipHealth;

    public GameObject victory;

    [Space(10)]
    public Notification notification;
    public GameObject notificationPrefabSimon;
    public GameObject notificationPrefabAlien;
    public GameObject notificationPrefabAlienQTE;
    public GameObject notificationPrefabRobot;
    public GameObject notificationPrefabFuel;
    public GameObject notificationPrefabAsteroid;

    void Start()
    {
        timer = 0f;
        numberOfCycles = 0;
        cycleUI.text = "Cycle " + numberOfCycles;
        spaceshipHealth = FindObjectOfType<SpaceshipHealth>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        _ktimer += Time.deltaTime;
        _atimer += Time.deltaTime;
        _qtimer += Time.deltaTime;
        _aitimer += Time.deltaTime;
        _itimer += Time.deltaTime;
        DisplayCycle(timer);
        MiniGameStarters(timer);

        switch (numberOfCycles)
        {
            case 4:
                if (!rpcCooldown)
                {
                    photonView.RPC("ChangeDifficulty", RpcTarget.AllBuffered, keyCodeLenght + 1, qTEBarTime + 2f, simonSaysLenght);
                    rpcCooldown = true;
                }
                break;

            case 8:
                if (!rpcCooldown)
                {
                    photonView.RPC("ChangeDifficulty", RpcTarget.AllBuffered, keyCodeLenght, qTEBarTime + 2f, simonSaysLenght + 1);
                    rpcCooldown = true;
                }
                break;

            case 12:
                if (!rpcCooldown)
                {
                    photonView.RPC("ChangeDifficulty", RpcTarget.AllBuffered, keyCodeLenght + 2, qTEBarTime + 2f, simonSaysLenght);
                    rpcCooldown = true;
                }
                break;

            case 16:
                if (!rpcCooldown)
                {
                    photonView.RPC("ChangeDifficulty", RpcTarget.AllBuffered, keyCodeLenght, qTEBarTime + 2f, simonSaysLenght + 1);
                    rpcCooldown = true;
                }
                break;

            case 20:
                if (!rpcCooldown)
                {
                    photonView.RPC("ChangeDifficulty", RpcTarget.AllBuffered, keyCodeLenght + 1, qTEBarTime + 2f, simonSaysLenght);
                    rpcCooldown = true;
                }
                break;

            case 14:
                victory.SetActive(true);
                photonView.RPC(nameof(Victory), RpcTarget.All);
                break;

            default:
                rpcCooldown = false;
                break;
        }

        if (robotOver)
        {
            _rtimer += Time.deltaTime;
            if (robotTimer >= 10)
            {
                miniGames.UnlockMiniGame("simon");
                notification.notificationPrefab = notificationPrefabSimon;
                notification.NotificationInput();
                _rtimer = 0;
                robotOver = false;
            }
        }
        else
        {
            _rtimer += Time.deltaTime;
            if (robotTimer >= 30)
            {
                _rtimer = 0;
                miniGames.UnlockMiniGame("robot");
                notification.notificationPrefab = notificationPrefabRobot;
                notification.NotificationInput();
            }
        }
    }

    public void MiniGameStarters(float time)
    {
        photonView.RPC(nameof(GameTimers), RpcTarget.AllBuffered, _ktimer, _atimer, _qtimer, _aitimer, _itimer, _rtimer);

        if (asteroidTimer % 45 == 0 && asteroidTimer != 0 && !asteroidCooldown)
        {
            miniGames.UnlockMiniGame("asteroid");
            notification.notificationPrefab = notificationPrefabAsteroid;
            notification.NotificationInput();
            asteroidCooldown = true;
        }
        /*else if (keyCodeTimer % 120 == 0 && keyCodeTimer != 0)
        {
            miniGames.UnlockMiniGame("keycode");
        }*/
        else if(qteTimer % 80 == 0 && qteTimer != 0 && !qteCooldown)
        {
            miniGames.UnlockMiniGame("qte");
            notification.notificationPrefab = notificationPrefabFuel;
            notification.NotificationInput();
            qteCooldown = true;
        }
        else if (alienQteTimer % 60 == 0 && alienQteTimer != 0 && !alienQTECooldown)
        {
            miniGames.UnlockMiniGame("alienQTE");
            notification.notificationPrefab = notificationPrefabAlienQTE;
            notification.NotificationInput();
            alienQTECooldown = true;
        }
        else if (keyCodeTimer % 120 == 0 && keyCodeTimer != 0 && !keycodeCooldown)
        {
            miniGames.UnlockMiniGame("keycode");
            notification.notificationPrefab = notificationPrefabAlienQTE;
            notification.NotificationInput();
            keycodeCooldown = true;
        }
        else if (invasionTimer % 150 == 0 && invasionTimer != 0 && !alienCooldown)
        {
            pool.SetActive(true);
            ResetTimer("invasion");
            notification.notificationPrefab = notificationPrefabAlien;
            notification.NotificationInput();
            alienCooldown = true;
        }

        if (asteroidTimer % 65 == 0 && asteroidTimer != 0 && miniGames.asteroidButton.activeInHierarchy)
        {
            miniGames.LockMiniGame("asteroid");
            ResetTimer("asteroid");
            spaceshipHealth.DamageHealth(10);
        }
        else if (keyCodeTimer % 140 == 0 && keyCodeTimer != 0 && miniGames.keycodeButton.activeInHierarchy)
        {
            miniGames.LockMiniGame("keycode");
            ResetTimer("keycode");
            spaceshipHealth.DamageHealth(10);
        }
        else if (qteTimer % 100 == 0 && qteTimer != 0 && miniGames.qteButton.activeInHierarchy)
        {
            miniGames.LockMiniGame("qte");
            ResetTimer("qte");
            spaceshipHealth.DamageHealth(10);
        }
        else if (alienQteTimer % 80 == 0 && alienQteTimer != 0 && miniGames.alienButton.activeInHierarchy)
        {
            miniGames.LockMiniGame("alienQTE");
            ResetTimer("alienQTE");
            pool.SetActive(true);
            ResetTimer("invasion");
            ScreenShake.instance.StartShake(.2f, .1f);
            spaceshipHealth.DamageHealth(10);
            notification.notificationPrefab = notificationPrefabAlien;
            notification.NotificationInput();
        }
    }

    public void ResetTimer(string name)
    {
        photonView.RPC(nameof(Cooldown), RpcTarget.AllBuffered, name);
    }

    public void DisplayCycle(float time)
    {
        floatToInt = Mathf.FloorToInt(time % 60);

        if (floatToInt % 30 == 0)
        {
            if (!cooldown)
            {
                numberOfCycles++;
                cycleUI.text = "Cycle " + numberOfCycles;
                cooldown = true;
            }
        }
        else
        {
            cooldown = false;
        }
    }

    [PunRPC]
    public void ChangeDifficulty(int kcLength, float qteTime, int ssLenght)
    {
        keyCodeLenght = kcLength;
        qTEBarTime = qteTime;
        simonSaysLenght = ssLenght;
    }

    [PunRPC]
    public void GameTimers(float _ktimer, float _atimer, float _qtimer, float _aitimer, float _itimer, float _rtimer)
    {
        keyCodeTimer = (int)_ktimer;
        asteroidTimer = (int)_atimer;
        qteTimer = (int)_qtimer;
        alienQteTimer = (int)_aitimer;
        invasionTimer = (int)_itimer;
        robotTimer = (int)_rtimer;
    }

    [PunRPC]
    void Cooldown(string name)
    {
        switch (name)
        {
            case "asteroid":
                _atimer = 0;
                break;

            case "keycode":
                _ktimer = 0;
                break;

            case "qte":
                _qtimer = 0;
                break;

            case "alienQTE":
                _aitimer = 0;
                break;

            case "invasion":
                _itimer = 0;
                break;

            default:
                break;
        }
    }

    [PunRPC]
    void Victory()
    {
        Time.timeScale = 0;
    }
}
