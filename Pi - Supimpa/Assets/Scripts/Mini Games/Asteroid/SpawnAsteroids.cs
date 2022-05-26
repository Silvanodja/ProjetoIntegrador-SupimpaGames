using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpawnAsteroids : MonoBehaviour
{
    public List<GameObject> asteroidsNaTela = new List<GameObject>();
    List<GameObject> atual = new List<GameObject>();
    public GameObject pasta;
    public float timer = 0f;
    public GameObject left, right;
    Vector3 pos;

    public GameObject miniGame, spaceShip;

    public CameraController gameCamera;

    public float minTimeThisEvent = 20f;
    public float maxTimeThisEvent = 40f;
    Vector3 extremidades;
    void Start()
    {
        // pos = left.transform.position;
    }

    private void Update()
    {
        extremidades = Camera.main.WorldToViewportPoint(transform.position);
    }
    private void Awake()
    {
       for (int j = 0; j < asteroidsNaTela.Count; j++)
        {
            //pos.x = Random.Range(extremidades.x = 0, extremidades.x = 1);

            var instanceAsteroid = Instantiate(asteroidsNaTela[j]);
            

            instanceAsteroid.transform.SetParent(pasta.transform);

            instanceAsteroid.SetActive(false);
            atual.Add(instanceAsteroid);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < atual.Count; i++)
        {
            atual[i].transform.localScale = new Vector2(1, 1);
        }
        StartCoroutine(TimerToEnd());
        StartCoroutine(Ativar(atual));

    }

    private void OnDisable()
    {
        gameCamera.miniGameIsPlaying = false;
    }

    IEnumerator Ativar(List<GameObject> ativar)
    {

        for (int i = 0; i < ativar.Count; i++)
        {
        //Vector3 spawn = new Vector3(Random.Range(extremidades.x = 0, extremidades.x = 1), 0.9f, 1);
            //if (!ativar[i].activeInHierarchy)
            //{
                yield return new WaitForSeconds(timer);
                //ativar[i].transform.position = Camera.main.ViewportToWorldPoint(spawn);
                ativar[i].SetActive(true);
            //}

        }
    }

    IEnumerator TimerToEnd()
    {
        yield return new WaitForSeconds(Random.Range(minTimeThisEvent, maxTimeThisEvent));
        if (!spaceShip.activeInHierarchy)
        {
            Debug.Log("Fracassou");
        }
        else
        {
            Debug.Log("Evento Concluido");
            miniGame.SetActive(false);
            
        }
    }
}
