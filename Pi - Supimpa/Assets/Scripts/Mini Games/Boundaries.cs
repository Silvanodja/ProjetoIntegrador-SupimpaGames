using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundaries : MonoBehaviour
{
    BoxCollider2D myCollider;

    void Start()
    {
        myCollider = gameObject.GetComponent<BoxCollider2D>();
    }


    void OnTriggerExit2D(Collider2D other)
    {
        var exitingObj = other.transform;
        var position1 = exitingObj.position;

        var boundaryPosition = transform.position;
        var colliderSize = myCollider.bounds.extents;

        if (position1.x > (boundaryPosition.x + colliderSize.x) || position1.x < (boundaryPosition.x - colliderSize.x))
        {
            position1.x = position1.x * (-1);
        }

        if (position1.y > (boundaryPosition.y + colliderSize.y) || position1.y < (boundaryPosition.y - colliderSize.y))
        {
            position1.y = position1.y * (-1);
        }


        exitingObj.position = position1;
    }
}
