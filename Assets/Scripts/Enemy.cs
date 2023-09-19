using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;
using System.Runtime.CompilerServices;

public class Enemy
{

}
//concrete command
public class FireGunCommand : ICommand
{
    private GameManager manager;
    public FireGunCommand(GameManager manager)
    {
        this.manager = manager;
    }
    //dit is de uitvoering van de concrete command
    public void Execute()
    {
        FireGun();
    }

    public void FireGun()
    {
        Debug.Log("gun fired");
        GameObject bullet = manager.GetPooledObjects();

        if(bullet != null){
            //set position
            //bullet.transform.position = 
            bullet.SetActive(true);
        }
    }
}

public class TestMessage : ICommand
{
    private GameManager manager;
    public TestMessage(GameManager manager)
    {
        this.manager = manager;
    }
    public void Execute()
    {
        Message();

    }

    public void Message()
    {
        manager.InstantiatedObjects.TryGetValue("player", out GameObject Player);

        Debug.Log("dit is de player positie: " + Player.transform.position);
    }
}
