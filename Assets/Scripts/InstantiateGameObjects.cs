using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InstantiateGameObjects
{

    private GameManager manager;
    public InstantiateGameObjects(GameManager manager)
    {
        this.manager = manager;
    }

    public void InstantiateObjects()
    {
        //voeg gameobjecten toe aan je dictionary
        manager.PrefabLibrary.Add("player", manager.PreFab);
        manager.PrefabLibrary.Add("enemy", manager.PreFab);

        //instantiaten van alle objecten in de dictionary
        foreach (var kvp in manager.PrefabLibrary)
        {
            GameObject instantiatedObject = GameObject.Instantiate(kvp.Value);
            instantiatedObject.name = kvp.Key; // optioneel, maar netjes: verander de naam van het gameobject in de wereld naar de kvp waarde(string)
            //hier de instantiated object toevoegden aan de library
            manager.InstantiadedObjects.Add(kvp.Key, instantiatedObject);
        }
    }
}
