using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Enemy : IPoolable
{
    //iedere keer dat dit wordt aangeroepen
    public bool Active { get; set; }

    public event Action<Enemy> OnDie;

//deze functie checkt of "ondie" event niet null is met de ?. daarna
    public void Die()
    {
        OnDie?.Invoke(this);
    }

    public void onDisableObject()
    {
        Debug.Log("object disabled and moved to different spot");
        //hier wordt het event weer geleegd.
        OnDie = null;
    }

    public void onEnableObject()
    {
        Debug.Log("Object enabled and moved to different spot");
    }
}
