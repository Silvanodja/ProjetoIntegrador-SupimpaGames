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
    public string miniGameName;

    void Start()
    {
        gameIsActive = false;
    }

    void Update()
    {
        switch (miniGameName)
        {
            case "simon":
                if (!gameIsActive) 
                {
                    gameIsActive = true;
                    photonView.RPC(nameof(MiniGameIsBeingPlayed), RpcTarget.OthersBuffered, miniGameName);
                }
                break;

            case "qte":
                if (!gameIsActive)
                {
                    gameIsActive = true;
                    photonView.RPC(nameof(MiniGameIsBeingPlayed), RpcTarget.OthersBuffered, miniGameName);
                }
                break;

            case "robot":
                if (!gameIsActive)
                {
                    gameIsActive = true;
                    photonView.RPC(nameof(MiniGameIsBeingPlayed), RpcTarget.OthersBuffered, miniGameName);
                }
                break;

            case "keycode":
                if (!gameIsActive)
                {
                    gameIsActive = true;
                    photonView.RPC("MiniGameIsBeingPlayed", RpcTarget.OthersBuffered, miniGameName);
                }
                break;

            default:
                gameIsActive = false;
                break;
        }
    }

    public void UnlockMiniGame(GameObject miniGame)
    {
        gameIsActive = false;
        photonView.RPC(nameof(MiniGameIsAvailable), RpcTarget.AllBuffered, miniGame);
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

            default:
                break;
        }
    }

    [PunRPC]
    void MiniGameIsAvailable(GameObject miniGame)
    {
        miniGame.SetActive(true);
    }
}
