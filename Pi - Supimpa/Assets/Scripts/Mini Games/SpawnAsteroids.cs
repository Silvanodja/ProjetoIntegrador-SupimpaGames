using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    List<GameObject> asteroidsNaTela = new List<GameObject>();
    List<int> atual = new List<int>();
    public string[] nomes;
    public GameObject[] aparece;
    float spawn = 1f;    
    public GameObject asteroid;

    GameObject left, right;

    private void Awake()
    {
        left = new GameObject("MinPosition");
        right = new GameObject("MaxPosition");
    }


    void Start()
    {
        Sorteio();
    }

    void Sorteio()
    {
        Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));

        left.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2, topRightScreenPoint.y + 1, 0);
        right.transform.position = new Vector3((bottomLeftScreenPoint.x - topRightScreenPoint.x) / 2 * -1, topRightScreenPoint.y + 1, 0);

        atual.Add(Random.Range(0, nomes.Length));
        Invoke("Sorteio", spawn);
        Vector3 pos = left.transform.position;
        pos.x = Random.Range(left.transform.position.x, right.transform.position.x);
        asteroid = Instantiate(aparece[atual[atual.Count - 1]], pos, left.transform.rotation);
        asteroidsNaTela.Add(asteroid);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
