using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class AlienInvasionQTE : MonoBehaviour
{
    bool flag;
    bool stop;
    public float timer;
    public float aux;
    public MiniGameManager miniManager;
    public CycleManager cycleManager;
    public CameraController gameCamera;
    public TMP_Text text;
    public EnemyPool pool;
    List<GameObject> dieEnemys = new();
    private void OnEnable()
    {
        pool = FindObjectOfType<EnemyPool>();
        aux = timer;
        flag = false;
        stop = false;
        miniManager.miniGameName = "alienQTE";
        text.gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        cycleManager.ResetTimer("alienQTE");
        cycleManager.alienQTECooldown = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!stop)
        {
            timer -= Time.deltaTime;
        }

        if (flag && timer > 0)
        {
            stop = true;
            FindObjectOfType<AudioManager>().Play("ButtonCorrect");
            StartCoroutine(Delay());
        }
        else if (timer <= 0)
        {
            stop = true;
            FindObjectOfType<AudioManager>().Play("ButtonFail");
            pool.photonView.RPC("RespawnEnemies", RpcTarget.All);
            StartCoroutine(Delay());
        }

    }

    public void StopTheInvasion()
    {
        flag = true;
    }

    IEnumerator Delay()
    {
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        FindObjectOfType<AudioManager>().Stop("MiniGameTheme");
        if (FindObjectOfType<AudioManager>().isPlaying("Invasion"))
        {
            FindObjectOfType<AudioManager>().Play("Invasion");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("MainTheme");
        }
        gameCamera.miniGameIsPlaying = false;
        miniManager.miniGameName = "default";
        cycleManager.ResetTimer("alienQTE");
        timer = aux;
        gameObject.SetActive(false);
    }
}
