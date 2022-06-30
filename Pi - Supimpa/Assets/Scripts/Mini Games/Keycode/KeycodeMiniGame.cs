using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeycodeMiniGame : MonoBehaviour
{
    public TMP_Text hiddenCode;
    public TMP_Text inputCode;
    public int codeLenght;
    public float codeResetTimeInSeconds = 0.5f;
    private bool isReseting = false;
    public CameraController gameCamera;
    public MiniGameManager miniManager;
    [SerializeField] CycleManager difficulty;

    private void OnEnable()
    {
        string code = string.Empty;
        codeLenght = difficulty.keyCodeLenght;
        for (int i = 0; i < codeLenght; i++)
        {
            code += Random.Range(1, 10);
        }

        hiddenCode.text = code;
        inputCode.text = string.Empty;
        miniManager.miniGameName = "keycode";
    }

    private void OnDisable()
    {
        miniManager.miniGameName = "default";
        difficulty.ResetTimer("keycode");
    }

    private void Update()
    {
    }

    public void ButtonClick(int number)
    {
        if (isReseting)
        {
            return;
        }

        inputCode.text += number;

        if (inputCode.text == hiddenCode.text)
        {
            inputCode.text = "Correct";
            FindObjectOfType<AudioManager>().Play("ButtonCorrect");
            StartCoroutine(ResetCodeWin());
        }
        else if (inputCode.text.Length >= codeLenght)
        {
            inputCode.text = "Failed";
            FindObjectOfType<AudioManager>().Play("ButtonFail");
            StartCoroutine(ResetCodeFail());
        }
    }

    private IEnumerator ResetCodeWin()
    {
        isReseting = true;

        yield return new WaitForSeconds(codeResetTimeInSeconds);

        inputCode.text = string.Empty;
        isReseting = false;
        gameObject.SetActive(false);
        gameCamera.miniGameIsPlaying = false;
        FindObjectOfType<AudioManager>().Stop("MiniGameTheme");
        if (FindObjectOfType<AudioManager>().isPlaying("Invasion"))
        {
            FindObjectOfType<AudioManager>().Play("Invasion");
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("MainTheme");
        }
    }

    private IEnumerator ResetCodeFail()
    {
        isReseting = true;

        yield return new WaitForSeconds(codeResetTimeInSeconds);

        inputCode.text = string.Empty;
        isReseting = false;
    }
}
