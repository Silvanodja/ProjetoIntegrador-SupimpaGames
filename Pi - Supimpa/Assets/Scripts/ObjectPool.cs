using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public GameObject bulletPrefab, pasta;
    public int amount = 5;

    List<GameObject> instances = new List<GameObject>();

        
    int lastInstanceIndex = 0;
    private void Awake()
    {
        for (int i = 0; i < amount; i++)
        {
            var instanceBullet = Instantiate(bulletPrefab);
            
            //bulletPrefab.transform.parent = pasta.transform;
            
            instanceBullet.SetActive(false);
            instanceBullet.transform.SetParent(pasta.transform);
            instances.Add(instanceBullet);
            
        }
    }

    public GameObject GetInstance()
    {
        GameObject instance = instances[lastInstanceIndex++];
        if(lastInstanceIndex >= instances.Count)
        {
            lastInstanceIndex = 0;
        }

        return instance;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
