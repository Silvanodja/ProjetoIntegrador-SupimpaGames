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

    private void OnEnable()
    {
        
    }

    private void Awake()
    {
        //Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        //Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        ////myCollider.size = new Vector3(myCollider.size.x - 0.1f, myCollider.size.y - 0.1f, 0);
        //gameObject.transform.localScale = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x) - 2, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y) - 2, 0f);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        var exitingObj = other.transform;
        var position1 = exitingObj.position;

        var boundaryPosition = transform.position;
        var colliderSize = myCollider.bounds.extents;

        if (position1.x > (boundaryPosition.x + colliderSize.x) || position1.x < (boundaryPosition.x - colliderSize.x))
        {
            position1.x = position1.x * -1;
        }

        if (position1.y > (boundaryPosition.y + colliderSize.y) || position1.y < (boundaryPosition.y - colliderSize.y))
        {
            position1.y = position1.y * -1;
            //print(position1.y);
        }


        exitingObj.position = position1;
    }

    
}
