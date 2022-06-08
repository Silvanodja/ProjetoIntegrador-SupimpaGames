using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RobotMinigame : MonoBehaviour
{
    public float distance;

    public Canvas canvas;
    public bool isConnected;

    [Space(10)]
    public RectTransform spawnPoint;
    public RectTransform conector;

    [Range(0.1f, 10f)]
    public float minimunConectorDistance = 10f;

    private void OnEnable()
    {
        isConnected = false;
        transform.position = spawnPoint.position;
    }

    void Update()
    {
        if (!isConnected)
        {
            distance = Vector2.Distance(transform.position, conector.position);
        }

        if (distance < minimunConectorDistance)
        {
            transform.position = conector.position;
            isConnected = true;
        }
    }

    public void DragHandler(BaseEventData data)
    {
        PointerEventData pointerData = (PointerEventData)data;
        Vector2 position;

        if (!isConnected) 
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform, pointerData.position,
            canvas.worldCamera, out position);

            transform.position = canvas.transform.TransformPoint(position); 
        }
    }
}
