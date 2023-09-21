using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ObjectPool
{
    private GameManager manager;

    public ObjectPool(GameManager manager)
    {
        this.manager = manager;
    }
    
    public GameObject GetPooledObjects()
    {
        if (manager.InactivePooledObjects.Count > 0)
        {
            if (!manager.InactivePooledObjects[0].activeInHierarchy && FireGunCommand._canFire)
            {
                GameObject _object = manager.InactivePooledObjects[0];
                manager.ActivePooledObjects.Add(_object);
                manager.InactivePooledObjects.Remove(_object);
                return _object;
            }
        }
        else
        {
            Debug.Log("no more bullets");
        }
        return null;
    }


}