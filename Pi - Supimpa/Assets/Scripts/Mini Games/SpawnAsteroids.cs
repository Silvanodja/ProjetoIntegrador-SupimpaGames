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

    public float minTimeThisEvent = 20f;
    public float maxTimeThisEvent = 40f;

    void Start()
    {
        pos = left.transform.position;

    }


    private void Awake()
    {
        for (int j = 0; j < asteroidsNaTela.Count; j++)
        {
            pos.x = Random.Range(left.transform.position.x, right.transform.position.x);

            var instanceAsteroid = Instantiate(asteroidsNaTela[j]);

            instanceAsteroid.transform.SetParent(pasta.transform);

            instanceAsteroid.SetActive(false);
            atual.Add(instanceAsteroid);
        }


    }

    private void OnEnable()
    {
        StartCoroutine(TimerToEnd());
        spaceShip.transform.position = new Vector3(0, 0, 0);
        StartCoroutine(Ativar(atual));
    }

    IEnumerator Ativar(List<GameObject> ativar)
    {


        for (int i = 0; i < ativar.Count; i++)
        {
            if (!ativar[i].activeInHierarchy)
            {
                yield return new WaitForSeconds(timer);
                ativar[i].transform.position = new Vector3(Random.Range(left.transform.position.x, right.transform.position.x), left.transform.position.y, 0);
                ativar[i].SetActive(true);
            }

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
