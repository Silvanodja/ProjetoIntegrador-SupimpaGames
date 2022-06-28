using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QTE : MonoBehaviour
{
    [SerializeField] CycleManager difficulty;
    public CameraController gameCamera;
    public MiniGameManager miniManager;
    public Slider slider;
    public TMP_Text pressText;
    public TMP_Text correctText;
    public TMP_Text wrongText;
    public float timer = 0f;
    bool buttonEnabled;
    bool buttonPressed;
    float pressTime;
    float textInterval;

    [Space(10)]
    public float pressTimeMinimum;
    public float pressTimeMaximum;
    [Space(10)]
    public float textIntervalOne;
    public float textIntervalTwo;
    [Space(10)]

    //[Range(1f, 20f)]
    //public float timeLimit;

    [Range(0.1f, 1f)]
    public float playerTimeGain;

    [Range(0.1f, 1f)]
    public float playerTimeLost;

    private void OnEnable()
    {
        slider.value = 0f;
        slider.maxValue = difficulty.qTEBarTime;
        timer = 0f;
        buttonEnabled = false;
        buttonPressed = false;
        pressText.enabled = false;
        correctText.enabled = false;
        wrongText.enabled = false;

        pressTime = RandomizeIntervals(pressTimeMinimum, pressTimeMaximum);
        textInterval = RandomizeIntervals(textIntervalOne, textIntervalTwo);
        miniManager.miniGameName = "qte";
    }

    private void OnDisable()
    {
        miniManager.miniGameName = "default";
        difficulty.ResetTimer("qte");
    }

    void Update()
    {
        if (timer <= difficulty.qTEBarTime)
        {
            timer += Time.deltaTime;
            slider.value = timer;

            if (!pressText.IsActive())
            {
                textInterval -= Time.deltaTime;

                if (textInterval < 0)
                {
                    pressText.enabled = true;
                    textInterval = RandomizeIntervals(textIntervalOne, textIntervalTwo);
                }
            }
            else
            {
                pressTime -= Time.deltaTime;
                buttonEnabled = true;

                if (pressTime < 0 || buttonPressed)
                {
                    pressText.enabled = false;
                    buttonEnabled = false;
                    buttonPressed = false;
                    pressTime = RandomizeIntervals(pressTimeMinimum, pressTimeMaximum);
                }
            }
        }
        else
        {
            timer = 0;
            FindObjectOfType<AudioManager>().Stop("MiniGameTheme");
            FindObjectOfType<AudioManager>().Play("MainTheme");
            gameCamera.miniGameIsPlaying = false;
            gameObject.SetActive(false);
        }
    }

    public void ButtonPress()
    {
        if (buttonEnabled)
        {
            timer += playerTimeGain;
            buttonPressed = true;
            StartCoroutine(CorrectOrWrong(correctText));
        }
        else
        {
            timer -= playerTimeLost;
            StartCoroutine(CorrectOrWrong(wrongText));
            if (timer < 0)
            {
                timer = 0;
            }
        }
    }

    float RandomizeIntervals(float min, float max)
    {
        return Random.Range(min, max);
    }

    IEnumerator CorrectOrWrong(TMP_Text text)
    {
        text.enabled = true;
        yield return new WaitForSeconds(1f);
        text.enabled = false;
    }
}
