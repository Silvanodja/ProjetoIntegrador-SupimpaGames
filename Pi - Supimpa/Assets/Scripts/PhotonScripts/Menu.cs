using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Menu : MonoBehaviourPunCallbacks
{
    [SerializeField] private LoginMenu loginMenu;
    [SerializeField] private LobbyMenu lobbyMenu;

    private void Start()
    {
        loginMenu.gameObject.SetActive(false);
        lobbyMenu.gameObject.SetActive(false);
    }

    public override void OnConnectedToMaster()
    {
        loginMenu.gameObject.SetActive(true);
    }

    public override void OnJoinedRoom()
    {
        ChangeMenu(lobbyMenu.gameObject);
        lobbyMenu.photonView.RPC("UpdateList", RpcTarget.All);
    }

    public void ChangeMenu(GameObject menu)
    {
        loginMenu.gameObject.SetActive(false);
        lobbyMenu.gameObject.SetActive(false);

        menu.SetActive(true);
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        lobbyMenu.UpdateList();
    }

    public void ExitLobby()
    {
        NetworkManager.Instance.ExitLobby();
        ChangeMenu(loginMenu.gameObject);
    }

    public void StartGame(string sceneName)
    {
        NetworkManager.Instance.photonView.RPC("StartGame", RpcTarget.All, sceneName);
    }
}
