using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class LobbyManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField createText; 
    public TMP_InputField joinText;

    public void OnCreate()
    {
        PhotonNetwork.CreateRoom(createText.text);
    }

    public void OnJoin()
    {
        PhotonNetwork.JoinRoom(joinText.text);
    }

    public override void OnJoinedRoom()
    {
        PlayerPrefs.DeleteAll();
        PhotonNetwork.LoadLevel("GabrielTeste");
    }
}
