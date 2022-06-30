using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public static NetworkManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
        FindObjectOfType<AudioManager>().Stop("MainMenu");
        FindObjectOfType<AudioManager>().Play("Lobby");
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connection successful");
    }

    public void _CreateRoom(string roomName)
    {
        PhotonNetwork.CreateRoom(roomName);
    }
    public void _FindRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void ChangeNick(string nickName)
    {
        PhotonNetwork.NickName = nickName;
    }

    public string GetPlayerList()
    {
        var list = "";

        foreach (var player in PhotonNetwork.PlayerList)
        {
            list += player.NickName + "\n";
        }
        return list;
    }
    public bool RoomBoss()
    {
        return PhotonNetwork.IsMasterClient;
    }

    public void ExitLobby()
    {
        PhotonNetwork.LeaveRoom();
    }

    [PunRPC]
    public void StartGame(string sceneName)
    {
        PhotonNetwork.LoadLevel(sceneName);
    }
}
