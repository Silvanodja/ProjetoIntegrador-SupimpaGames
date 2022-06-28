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
    public TMP_Text cycleUI;
    public int keyCodeLenght;
    public float qTEBarTime;
    public int simonSaysLenght;

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
        DisplayCycle(timer);

        switch (numberOfCycles)
        {
            case 2:
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

    public void DisplayCycle(float time)
    {
        floatToInt = Mathf.FloorToInt(time % 60);

        if (floatToInt % 60 == 0)
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
}
