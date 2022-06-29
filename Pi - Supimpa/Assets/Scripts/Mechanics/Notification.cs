using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Photon.Pun;

public class Notification : MonoBehaviourPunCallbacks
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
        initialPosition = new Vector3(notificationUI.transform.position.x - 80,
                        notificationUI.transform.position.y - 20,
                        notificationUI.transform.position.z);
        notificationPrefab.transform.localPosition = initialPosition;
    }

    void Update()
    {

    }

    [PunRPC]
    public void NotificationMaster(int i)
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
                    initialPosition.y - (25 * i),
                    initialPosition.z);
                notification.isOnScreen = true;
                StartCoroutine(DespawnNotification(notificationPrompt));
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

    public void NotificationInput()
    {
        photonView.RPC("NotificationMaster", RpcTarget.All, 0);
    }
}
