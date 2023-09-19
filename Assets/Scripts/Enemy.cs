using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

// public class Enemy : IPoolable
// {
//     //iedere keer dat dit wordt aangeroepen
//     public GameObject GameObject {get; set;}

//     public event Action<Enemy> OnDie;

//     //deze functie checkt of "ondie" event niet null is met de ?. daarna
//     public void Die()
//     {
//         OnDie?.Invoke(this);
//     }

//     public void onDisableObject()
//     {
//         Debug.Log("object disabled and moved to different spot");
//         //hier wordt het event weer geleegd.
//         OnDie = null;
//     }

//     public void onEnableObject()
//     {
//         Debug.Log("Object enabled and moved to different spot");
//     }
// }

//concrete command
public class FireGunCommand : ICommand
{
    //dit is de uitvoering van de concrete command
    public void Execute()
    {
        FireGun();
    }

    public void FireGun()
    {
        Debug.Log("gun fired");
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
        manager.InstantiadedObjects.TryGetValue("player", out GameObject Player);

        Debug.Log("dit is de player positie: " + Player.transform.position);
    }
}
