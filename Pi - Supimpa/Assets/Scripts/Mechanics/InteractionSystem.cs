using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractionSystem : MonoBehaviour
{
    public Transform detectionPoint;
    public float detectionRadius = 1f;
    public LayerMask detectionLayer;
    public GameObject miniGameAsteroids;

    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                //SceneManager.LoadScene("MarcosA");
                miniGameAsteroids.SetActive(true);
            }
        }
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        return Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(detectionPoint.position, detectionRadius);
    }
}
