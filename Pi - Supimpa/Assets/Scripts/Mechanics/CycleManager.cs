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
    [SerializeField]bool rpcCooldown;
    public float floatToInt;
    public float timer;
    float _ktimer;
    float _atimer;
    float _qtimer;
    float _aitimer;
    float _itimer;
    public int keyCodeTimer;
    public int asteroidTimer;
    public int qteTimer;
    public int alienQteTimer;
    public int invasionTimer;
    public TMP_Text cycleUI;
    public int keyCodeLenght;
    public float qTEBarTime;
    public int simonSaysLenght;
    public MiniGameManager miniGames;

    PhotonView view;

    void Start()
    {
        timer = 0f;
        numberOfCycles = 0;
        cycleUI.text = "Cycle " + numberOfCycles;
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

            default:
                rpcCooldown = false;
                break;
        }
    }

    public void MiniGameStarters(float time)
    {
        photonView.RPC(nameof(GameTimers), RpcTarget.AllBuffered, _ktimer, _atimer, _qtimer, _aitimer, _itimer);

        if (asteroidTimer % 45 == 0 && asteroidTimer != 0)
        {
            miniGames.UnlockMiniGame("asteroid");
        }
        else if (keyCodeTimer % 120 == 0 && keyCodeTimer != 0)
        {
            miniGames.UnlockMiniGame("keycode");
        }
        else if(qteTimer % 80 == 0 && qteTimer != 0)
        {
            miniGames.UnlockMiniGame("qte");
        }
        else if (alienQteTimer % 60 == 0 && alienQteTimer != 0)
        {
            miniGames.UnlockMiniGame("alienQTE");
        }

        if (asteroidTimer % 65 == 0 && asteroidTimer != 0 && miniGames.asteroidButton.activeInHierarchy)
        {
            miniGames.LockMiniGame("asteroid");
            ResetTimer("asteroid");
        }
        else if (keyCodeTimer % 140 == 0 && keyCodeTimer != 0 && miniGames.keycodeButton.activeInHierarchy)
        {
            miniGames.LockMiniGame("keycode");
            ResetTimer("keycode");
        }
        else if (qteTimer % 100 == 0 && qteTimer != 0 && miniGames.qteButton.activeInHierarchy)
        {
            miniGames.LockMiniGame("qte");
            ResetTimer("qte");
        }
        else if (alienQteTimer % 80 == 0 && alienQteTimer != 0 && miniGames.alienButton.activeInHierarchy)
        {
            miniGames.LockMiniGame("alienQTE");
            ResetTimer("alienQTE");
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
    public void GameTimers(float _ktimer, float _atimer, float _qtimer, float _aitimer, float _itimer)
    {
        keyCodeTimer = (int)_ktimer;
        asteroidTimer = (int)_atimer;
        qteTimer = (int)_qtimer;
        alienQteTimer = (int)_aitimer;
        invasionTimer = (int)_itimer;
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
}
