using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject miniGame;

    private void Start()
    {
        miniGame.SetActive(false);
    }
}
