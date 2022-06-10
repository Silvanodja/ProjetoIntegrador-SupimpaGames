using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pooling : MonoBehaviour
{
    public GameObject objectsToInstantiate;
    public int beginsInstantiated;

    public List<GameObject> listOfObjects;

    public bool canIncrease = true;

    void Start()
    {
        listOfObjects = new List<GameObject>();

        for (int i = 0; i < beginsInstantiated; i++)
        {
            listOfObjects.Add(Instantiate(objectsToInstantiate));
            listOfObjects[i].SetActive(false);
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

        listOfObjects.Add(Instantiate(objectsToInstantiate));
        return listOfObjects[listOfObjects.Count - 1];
    }
}
