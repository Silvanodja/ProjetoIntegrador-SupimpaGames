using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviourPunCallbacks
{
    public GameObject objectsToInstantiate;
    public int beginsInstantiated;
    public GameObject pasta;

    public List<GameObject> listOfObjects;

    public bool canIncrease = true;

    [SerializeField] private string prefabLocation;

    void Start()
    {
        listOfObjects = new List<GameObject>();
        photonView.RPC("CreateAmmunition", RpcTarget.MasterClient);
    }

    [PunRPC]
    private void CreateAmmunition()
    {
        for (int i = 0; i < beginsInstantiated; i++)
        {
            var bullet = PhotonNetwork.Instantiate(prefabLocation, Vector3.zero, Quaternion.identity);
            listOfObjects.Add(bullet);
            bullet.SetActive(false);
            bullet.transform.SetParent(pasta.transform);
        }
        photonView.RPC("DesactioveBullet", RpcTarget.Others);
    }
    [PunRPC]
    public void DesactioveBullet()
    {
        foreach (var item in GameObject.FindObjectsOfType<GunShot>())
        {
            listOfObjects.Add(item.gameObject);
            item.gameObject.SetActive(false);
        }
    }

    public GameObject GetObject()
    {
        for (int i = 0; i < listOfObjects.Count; i++)
        {
            if (!listOfObjects[i].activeInHierarchy)
            {
                return listOfObjects[i];
            }
        }

        if (!canIncrease)
        {
            return null;
        }

        listOfObjects.Add(PhotonNetwork.Instantiate(prefabLocation, Vector3.zero, Quaternion.identity));
        return listOfObjects[listOfObjects.Count - 1];
    }
}
