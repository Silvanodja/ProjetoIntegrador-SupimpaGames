using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    public bool begin;

    private void OnDisable()
    {
        begin = false;
    }
}
