using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AlienInvasionQTE : MonoBehaviour
{
    bool flag;
    bool stop;
    public float timer;
    public MiniGameManager miniManager;
    public CycleManager cycleManager;
    public CameraController gameCamera;
    public TMP_Text text;

    private void OnEnable()
    {
        flag = false;
        stop = false;
        miniManager.miniGameName = "alienQTE";
        text.gameObject.SetActive(false);
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
            StartCoroutine(Delay());
        }
        else if(timer <= 0)
        {
            stop = true;
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
        FindObjectOfType<AudioManager>().Play("MainTheme");
        gameCamera.miniGameIsPlaying = false;
        miniManager.miniGameName = "default";
        cycleManager.ResetTimer("alienQTE");
        gameObject.SetActive(false);
    }
}
