using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class CycleManager : MonoBehaviourPunCallbacks
{
    int numberOfCycles;
    public float timer;
    public TMP_Text cycleUI;
    public int keyCodeLenght;
    public float qTEBarTime;
    public int simonSaysLenght;

    void Start()
    {
        timer = 0f;
        numberOfCycles = 1;
        cycleUI.text = "Cycle " + numberOfCycles;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer % 60 == 0)
        {
            numberOfCycles++;
            cycleUI.text = "Cycle " + numberOfCycles;
        }

        switch (timer)
        {
            case 240:
                ChangeDifficulty(keyCodeLenght + 1, qTEBarTime + 2f, simonSaysLenght);
                break;

            case 480:
                ChangeDifficulty(keyCodeLenght, qTEBarTime + 2f, simonSaysLenght + 1);
                break;

            case 720:
                ChangeDifficulty(keyCodeLenght + 1, qTEBarTime + 2f, simonSaysLenght);
                break;

            case 960:
                ChangeDifficulty(keyCodeLenght, qTEBarTime + 2f, simonSaysLenght + 1);
                break;

            case 1200:
                ChangeDifficulty(keyCodeLenght + 1, qTEBarTime + 2f, simonSaysLenght);
                break;

            default:
                break;
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
