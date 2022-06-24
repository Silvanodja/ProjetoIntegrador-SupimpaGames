using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class LobbyMenu : MonoBehaviourPunCallbacks
{
    [SerializeField] private Text playerList;
    [SerializeField] private Button startGame;

    [PunRPC]
    public void UpdateList()
    {
        playerList.text = NetworkManager.Instance.GetPlayerList();
        startGame.interactable = NetworkManager.Instance.RoomBoss();
    }


}
