using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class KeycodeMiniGame : MonoBehaviour
{
    public TMP_Text hiddenCode;
    public TMP_Text inputCode;
    public int codeLenght;
    public float codeResetTimeInSeconds = 0.5f;
    private bool isReseting = false;
    public CameraController gameCamera;
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
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            FindObjectOfType<AudioManager>().Stop("MiniGameTheme");
            FindObjectOfType<AudioManager>().Play("MainTheme");
            gameCamera.miniGameIsPlaying = false;
            gameObject.SetActive(false);
        }
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
            StartCoroutine(ResetCodeWin());
        }
        else if (inputCode.text.Length >= codeLenght)
        {
            inputCode.text = "Failed";
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
        FindObjectOfType<AudioManager>().Play("MainTheme");
    }

    private IEnumerator ResetCodeFail()
    {
        isReseting = true;

        yield return new WaitForSeconds(codeResetTimeInSeconds);

        inputCode.text = string.Empty;
        isReseting = false;
    }
}
