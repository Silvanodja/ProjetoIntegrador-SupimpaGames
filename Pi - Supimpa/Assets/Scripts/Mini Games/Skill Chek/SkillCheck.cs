using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillCheck : MonoBehaviour
{
    private void Ontrigger2D(Collision other)
    {
        Debug.Log("Colisao");
    }
    private void OnTriggerStay2D(Collider2D other) {
        
        var enterObject = other.transform;
        print("Colidiu");

        if (other.gameObject.tag == "SkillChek")
        {
            print("Salve");
        }
    }
}
