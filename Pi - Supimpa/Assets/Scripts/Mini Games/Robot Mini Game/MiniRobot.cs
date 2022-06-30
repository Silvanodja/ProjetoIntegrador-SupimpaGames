using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniRobot : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Delay());
    }

    IEnumerator Delay()
    {
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
