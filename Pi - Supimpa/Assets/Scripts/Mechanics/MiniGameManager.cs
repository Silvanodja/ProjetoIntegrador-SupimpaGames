using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class MiniGameManager : MonoBehaviourPunCallbacks
{
    bool gameIsActive;
    public GameObject qteButton;
    public GameObject simonButton;
    public GameObject keycodeButton;
    public GameObject robotButton;
    public GameObject asteroidButton;
    public GameObject alienButton;
    public CycleManager cycleManager;
    public string miniGameName;

    void Start()
    {
        gameIsActive = false;
        qteButton.SetActive(false);
        simonButton.SetActive(false);
        keycodeButton.SetActive(false);
        robotButton.SetActive(true);
        asteroidButton.SetActive(false);
        alienButton.SetActive(false);
    }

    void Update()
    {
        switch (miniGameName)
        {
            case "simon":
                if (!gameIsActive) 
                {
                    gameIsActive = true;
                    photonView.RPC(nameof(MiniGameIsBeingPlayed), RpcTarget.AllBuffered, miniGameName);
                }
                break;

            case "qte":
                if (!gameIsActive)
                {
                    gameIsActive = true;
                    photonView.RPC(nameof(MiniGameIsBeingPlayed), RpcTarget.AllBuffered, miniGameName);
                }
                break;

            case "robot":
                if (!gameIsActive)
                {
                    gameIsActive = true;
                    photonView.RPC(nameof(MiniGameIsBeingPlayed), RpcTarget.AllBuffered, miniGameName);
                }
                break;

            case "keycode":
                if (!gameIsActive)
                {
                    gameIsActive = true;
                    photonView.RPC("MiniGameIsBeingPlayed", RpcTarget.AllBuffered, miniGameName);
                }
                break;

            case "asteroid":
                if (!gameIsActive)
                {
                    gameIsActive = true;
                    photonView.RPC("MiniGameIsBeingPlayed", RpcTarget.AllBuffered, miniGameName);
                }
                break;

            case "alienQTE":
                if (!gameIsActive)
                {
                    gameIsActive = true;
                    photonView.RPC("MiniGameIsBeingPlayed", RpcTarget.AllBuffered, miniGameName);
                }
                break;

            default:
                gameIsActive = false;
                break;
        }
    }

    public void UnlockMiniGame(string miniGame)
    {
        photonView.RPC(nameof(MiniGameIsAvailable), RpcTarget.AllBuffered, miniGame);
    }

    public void LockMiniGame(string miniGame)
    {
        photonView.RPC(nameof(MiniGameDisable), RpcTarget.AllBuffered, miniGame);
    }

    [PunRPC]
    void MiniGameIsBeingPlayed(string miniGame)
    {
        switch (miniGame)
        {
            case "simon":
                simonButton.SetActive(false);
                break;

            case "qte":
                qteButton.SetActive(false);
                break;

            case "robot":
                robotButton.SetActive(false);
                break;

            case "keycode":
                keycodeButton.SetActive(false);
                break;

            case "asteroid":
                asteroidButton.SetActive(false);
                break;

            case "alienQTE":
                alienButton.SetActive(false);
                break;

            default:
                break;
        }
    }

    [PunRPC]
    void MiniGameIsAvailable(string miniGame)
    {
        switch (miniGame)
        {
            case "simon":
                simonButton.SetActive(true);
                break;

            case "qte":
                qteButton.SetActive(true);
                break;

            case "robot":
                robotButton.SetActive(true);
                break;

            case "keycode":
                keycodeButton.SetActive(true);
                break;

            case "asteroid":
                asteroidButton.SetActive(true);
                break;

            case "alienQTE":
                alienButton.SetActive(true);
                break;

            default:
                break;
        }
    }

    [PunRPC]
    void MiniGameDisable(string miniGame)
    {
        switch (miniGame)
        {
            case "simon":
                simonButton.SetActive(false);
                break;

            case "qte":
                qteButton.SetActive(false);
                cycleManager.ResetTimer("qte");
                cycleManager.qteCooldown = false;
                break;

            case "robot":
                robotButton.SetActive(false);
                break;

            case "keycode":
                keycodeButton.SetActive(false);
                cycleManager.ResetTimer("keycode");
                cycleManager.keycodeCooldown = false;
                break;

            case "asteroid":
                asteroidButton.SetActive(false);
                break;

            case "alienQTE":
                alienButton.SetActive(false);
                break;

            default:
                break;
        }
    }
}
