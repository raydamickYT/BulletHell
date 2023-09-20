using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy
{

}
//concrete command
public class FireGunCommand : ICommand
{
    private GameManager manager;
    
    public static bool _canFire = true;
    public FireGunCommand(GameManager manager)
    {
        this.manager = manager;
    }
    //dit is de uitvoering van de concrete command
    public void Execute()
    {
        manager.UpdateBullet += UpdateBulletPos;
        FireGun();
    }

    public void FireGun()
    {
        //Debug.Log("gun fired");
        GameObject bullet = manager.GetPooledObjects();
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (bullet != null && _canFire)
        {
            //            Debug.Log(bullet);
            //set position
            //bullet.transform.position = 
            bullet.SetActive(true);
            Vector3 Direction = bullet.transform.forward + new Vector3(2, 2, 2);
            manager.RunCoroutine(Wait());
            manager.RunCoroutine(BulletLifeTime(bullet));
        }
    }

    public void UpdateBulletPos()
    {

    }

    public IEnumerator Wait()
    {
        _canFire = false;
        yield return new WaitForSeconds(.5f);
        _canFire = true;
    }
    public IEnumerator BulletLifeTime(GameObject bullet)
    {
        yield return new WaitForSeconds(1);
        manager.DeActivate(bullet);
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
