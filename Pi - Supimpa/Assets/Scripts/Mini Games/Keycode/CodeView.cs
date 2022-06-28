using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using TMPro;

public class CodeView : MonoBehaviourPunCallbacks
{
    public TMP_Text code;
    public KeycodeMiniGame game;
    bool flag;

    void Start()
    {
        //game = FindObjectOfType<KeycodeMiniGame>();   
    }

    void Update()
    {
        if (game.enabled && !flag)
        {
            photonView.RPC(nameof(ChangeCode), RpcTarget.AllBuffered, code.text);      
        }
        else if (!game.enabled)
        {
            flag = false;
        }
    }

    [PunRPC]
    void ChangeCode(string text)
    {
        code.text = text;
        flag = true;
    }
}
