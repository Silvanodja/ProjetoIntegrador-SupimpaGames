using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GameManagerScript : MonoBehaviourPunCallbacks
{
    public static GameManagerScript Instance { get; private set; }

    [SerializeField] private string prefabLocation;
    [SerializeField] private Transform[] spawns;
    public GameObject pool;
    private int playerInGame = 0;
    private List<PlayerController> players = new List<PlayerController>();
    public List<PlayerController> Players { get => players; private set => players = value; }
    public GameObject defeat;

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
        FindObjectOfType<AudioManager>().Stop("Lobby");
        FindObjectOfType<AudioManager>().Play("MainTheme");
        photonView.RPC("AddPlayer", RpcTarget.AllBuffered);
        Players = new List<PlayerController>();
       // pool.SetActive(false);
    }

    [PunRPC]
    public void AddPlayer()
    {
        playerInGame++;
        if(playerInGame == PhotonNetwork.PlayerList.Length)
        {
            CreatePlayer();
        }
    }
    private void CreatePlayer()
    {
        var playerObj = PhotonNetwork.Instantiate(prefabLocation, spawns[Random.Range(0, spawns.Length)].position, Quaternion.identity);
        var player = playerObj.GetComponent<PlayerController>();

        player.photonView.RPC("Initialize", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
    private void Update()
    {
        
    }
}
