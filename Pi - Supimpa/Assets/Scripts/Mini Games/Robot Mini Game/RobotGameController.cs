using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotGameController : MonoBehaviour
{
    public RobotMinigame[] bodyParts;
    int count;
    bool[] flags;
    bool itsOver;

    public CameraController gameCamera;

    private void OnEnable()
    {
        count = 0;
        itsOver = false;
        flags = new bool[5];
        for (int j = 0; j < flags.Length; j++)
        {
            flags[j] = false;
        }
    }

    void Update()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            if (bodyParts[i].isConnected && !flags[i])
            {
                count++;
                flags[i] = true;
            }
        }

        if (count == 5 && !itsOver)
        {
            StartCoroutine(Delay());
            itsOver = true;
        }
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<AudioManager>().Stop("MiniGameTheme");
        FindObjectOfType<AudioManager>().Play("MainTheme");
        gameCamera.miniGameIsPlaying = false;
        gameObject.SetActive(false);
    }
}
