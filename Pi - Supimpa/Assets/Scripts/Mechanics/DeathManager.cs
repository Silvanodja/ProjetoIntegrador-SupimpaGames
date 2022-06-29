using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class DeathManager : MonoBehaviourPunCallbacks
{
    public int count;
    public GameObject defeat;

    void Start()
    {
        
    }

    void Update()
    {
        if (count >= PhotonNetwork.PlayerList.Length)
        {
            defeat.SetActive(true);
            photonView.RPC(nameof(Defeat), RpcTarget.All);
        }
    }

    public void DeathCount(int i)
    {
        count += i;
    }

    [PunRPC]
    void Defeat()
    {
        Time.timeScale = 0;
    }
}
