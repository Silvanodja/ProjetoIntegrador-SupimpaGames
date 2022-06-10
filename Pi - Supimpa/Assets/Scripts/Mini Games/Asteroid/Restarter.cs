using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restarter : MonoBehaviour
{
    public GameObject nave, IinteriorDaNave, spawnAsteroids, vida, municao, player, arredores;
    public bool ativado = false;
    int oldMask;
    private void Start()
    {
        player = GetComponent<SpawnManager>().gm;
        oldMask = Camera.main.cullingMask;
    }
    private void Update()
    {
        if (ativado == false)
        {
            if (Input.GetKeyDown(KeyCode.M))
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
                Camera.main.cullingMask = ~(1 << 13);
                print(Camera.main.cullingMask);

                //player.GetComponent<Player>().enabled = false;
                ativado = true;
            }
        }
        else if (ativado == true)
        {
            if (Input.GetKeyDown(KeyCode.M))
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
                ativado = false;
            }
        }
    }

}

