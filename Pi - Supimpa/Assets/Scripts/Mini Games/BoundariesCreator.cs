using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundariesCreator : MonoBehaviour
{
    GameObject top;
    public GameObject tv, left, right;
    void Awake()
    {
        top = new GameObject("Boundaries");
    }
    // Start is called before the first frame update
    void Start()
    {
        CreateScreenColliders();
    }

    void CreateScreenColliders()
    {
        Vector3 bottomLeftScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(0f, 0f, 0f));
        Vector3 topRightScreenPoint = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0f));
        
        // Create top collider
        BoxCollider2D collider = top.AddComponent<BoxCollider2D>();
        Boundaries bordas = top.AddComponent<Boundaries>();
        collider.isTrigger = true;
        collider.size = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x) - 3, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y) - 3, 0f);
        collider.offset = new Vector2(0, 0);
        tv.transform.localScale = new Vector3(Mathf.Abs(bottomLeftScreenPoint.x - topRightScreenPoint.x) - 2, Mathf.Abs(topRightScreenPoint.y - bottomLeftScreenPoint.y) - 2, 0f);
        top.transform.position = new Vector3(0f, 0f, 0f);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
