using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public InputHandler inputHandler;
    //public ICommand command;
    public GameObject test;
    

    // Start is called before the first frame update
    void Start()
    {
        //setup de input handler op een game object.
        inputHandler = new InputHandler();
        //door een var aan te maken voor een van de acties, hoef je niet de hele tijd 'new firefuncommand()" te schrijven en kan je meerdere knoppen voor een action gebruiken.
        var firegun = new FireGunCommand();
        //deze line zorgt ervoor dat er een key wordt gebind aan de juiste actie. de actie zit op de actor
        inputHandler.BindInputToCommand(KeyCode.X, firegun);
        inputHandler.BindInputToCommand(KeyCode.Y, firegun);
        inputHandler.BindInputToCommand(KeyCode.R, new TestMessage());

    }

    // Update is called once per frame
    void Update()
    {
        inputHandler.HandleInput();

        // if (command != null)
        // {
        //     command.Execute();
        // }
    }
}
