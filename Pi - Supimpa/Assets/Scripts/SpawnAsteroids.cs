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
    int pontos = 0;
    public Transform posicaoMin, posicaoMax;
    public GameObject asteroid;

    // Start is called before the first frame update
    void Start()
    {
        Sorteio();
    }

    void Sorteio()
    {
        atual.Add(Random.Range(0, nomes.Length));
        Invoke("Sorteio", spawn);
        Vector3 pos = posicaoMin.position;
        pos.x = Random.Range(posicaoMin.position.x, posicaoMax.position.x);
        asteroid = Instantiate(aparece[atual[atual.Count - 1]], pos, posicaoMin.rotation);
        asteroidsNaTela.Add(asteroid);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
