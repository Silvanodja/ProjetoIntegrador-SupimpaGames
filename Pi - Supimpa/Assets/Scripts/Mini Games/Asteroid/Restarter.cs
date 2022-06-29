using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    public GameObject nave, IinteriorDaNave, spawnAsteroids, vida, municao, player;
    public bool ativado = false;
    int oldMask;
    public MiniGameManager miniManager;
    public CycleManager cycleManager;
    public float timer;
    public Interact start;

    private void Start()
    {
        player = GetComponent<SpawnManager>().gm;
        oldMask = Camera.main.cullingMask;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (ativado == false)
        {
            timer = 0;
            if (start.begin)
            {
                FindObjectOfType<AudioManager>().Play("MiniGameTheme");
                FindObjectOfType<AudioManager>().Pause("MainTheme");
                IinteriorDaNave.SetActive(false);
                nave.SetActive(true);
                municao.SetActive(true);
                spawnAsteroids.SetActive(true);
                vida.SetActive(true);
                nave.GetComponent<Spaceship>().enabled = true;
                Camera.main.orthographicSize = 170;
                Camera.main.cullingMask = ~(1 << 13) - (1 << 12) - (1 << 11) - (1 << 6);
                
                print(Camera.main.cullingMask);
                miniManager.miniGameName = "asteroid";

                //player.GetComponent<Player>().enabled = false;
                ativado = true;
            }
        }
        else if (ativado == true)
        {
            if (timer >= 10)
            {
                FindObjectOfType<AudioManager>().Stop("MiniGameTheme");
                FindObjectOfType<AudioManager>().Play("MainTheme");
                IinteriorDaNave.SetActive(true);
                nave.SetActive(false);
                municao.SetActive(false);
                spawnAsteroids.SetActive(false);
                vida.SetActive(false);
                nave.GetComponent<Spaceship>().enabled = false;
                Camera.main.orthographicSize = 7;
                Camera.main.cullingMask = oldMask;
                Camera.main.transform.position = new Vector3(0, 0, -10);
                //player.GetComponent<Player>().enabled = true;
                miniManager.miniGameName = "default";
                cycleManager.ResetTimer("asteroid");
                ativado = false;
            }
        }
    }

}

