using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//scirpt voor het maken van de game objecten
public class InstantiateGameObjects
{
    //dependency injection (dit geval met de gamemanager monobehaviour)
    private GameManager manager;
    public InstantiateGameObjects(GameManager manager)
    {
        this.manager = manager;
    }

    public void InstantiateObjects()
    {
        //voeg gameobjecten toe aan je dictionary
        manager.PrefabLibrary.Add("player", manager.PreFab);
        //manager.PrefabLibrary.Add("Bullet", manager.PreFab);

        //voeg bullets toe aan de dictionary
        for (int i = 0; i < manager.AmountToPool; i++)
        {
            manager.PrefabLibrary.Add("Bullet" + i.ToString(), manager.PreFab);
        }
        //manager.PrefabLibrary.Add("enemy", manager.PreFab);

        //instantiaten van alle objecten in de dictionary
        foreach (var kvp in manager.PrefabLibrary)
        {
            Vector3 testPos = new Vector3(0, 0, 0);
            //check welk object er gespawned wordt en verander de positie afhankelijk van andere objecten
            if (kvp.Key == "player")
            {
                testPos = new Vector3(0, -4.4f, 0);
            }

            GameObject instantiatedObject = GameObject.Instantiate(kvp.Value, testPos, Quaternion.identity);
            instantiatedObject.name = kvp.Key; // optioneel, maar netjes: verander de naam van het gameobject in de wereld naar de kvp waarde(string)

            //doe wat logic om de bullets inactive te maken en toe te voegen aan de object pool.
            if(kvp.Key.StartsWith("Bullet")){
                instantiatedObject.SetActive(false);
                manager.InactivePooledObjects.Add(instantiatedObject);
            }

            //hier de instantiated object toevoegden aan de library
            manager.InstantiatedObjects.Add(kvp.Key, instantiatedObject);
        }
    }
}
