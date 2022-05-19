using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Notification : MonoBehaviour
{
    public class NotificationClass
    {
        public GameObject prompt;
        public bool isOnScreen;

        public NotificationClass(GameObject pr, bool on)
        {
            prompt = pr;
            isOnScreen = on;
        }
    }

    [SerializeField] Queue<NotificationClass> notificationList = new Queue<NotificationClass>();
    GameObject notificationPrompt;
    NotificationClass notification;
    public Canvas notificationUI;
    public GameObject notificationPrefab;
    Vector3 initialPosition;


    void Start()
    {
        initialPosition = new Vector3(notificationUI.transform.position.x + 155,
                        notificationUI.transform.position.y - 30,
                        notificationUI.transform.position.z);
        notificationPrefab.transform.localPosition = initialPosition;
    }

    void Update()
    {
        int i = 0;
        if (NotificationInput())
        {
            notification = new NotificationClass(notificationPrefab, false);
            notificationList.Enqueue(notification);

            foreach (NotificationClass item in notificationList)
            {
                i++;
                if (!item.isOnScreen)
                {
                    notificationPrompt = Instantiate(notificationPrefab, notificationUI.transform);
                    notificationPrefab.transform.localPosition = new Vector3(initialPosition.x,
                        initialPosition.y - (55 * i),
                        initialPosition.z);
                    notification.isOnScreen = true;
                    StartCoroutine(DespawnNotification(notificationPrompt));
                }
            }
        }
    }

    IEnumerator DespawnNotification(GameObject notification)
    {
        yield return new WaitForSeconds(5f);
        notificationList.Dequeue();
        notificationPrefab.transform.localPosition = initialPosition;
        Destroy(notification);
    }

    bool NotificationInput()
    {
        return Input.GetKeyDown(KeyCode.Space);
    }
}
