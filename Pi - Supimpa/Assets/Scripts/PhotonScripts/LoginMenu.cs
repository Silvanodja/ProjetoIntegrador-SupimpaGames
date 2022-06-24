using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour
{
    [SerializeField] private Text playerName;
    [SerializeField] private Text roomName;

    public void _CreateRoom()
    {
        NetworkManager.Instance._CreateRoom(roomName.text);
        NetworkManager.Instance.ChangeNick(playerName.text);
    }
    public void _FindRoom()
    {
        NetworkManager.Instance._FindRoom(roomName.text);
        NetworkManager.Instance.ChangeNick(playerName.text);
    }
}
