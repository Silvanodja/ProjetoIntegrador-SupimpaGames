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

    [PunRPC]
    void Start()
    {
        listOfObjects = new List<GameObject>();
        CreateAmmunition();
    }

    private void CreateAmmunition()
    {
        for (int i = 0; i < beginsInstantiated; i++)
        {
            listOfObjects.Add(PhotonNetwork.Instantiate(prefabLocation, Vector3.zero, Quaternion.identity));
            listOfObjects[i].SetActive(false);
            listOfObjects[i].transform.SetParent(pasta.transform);
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
