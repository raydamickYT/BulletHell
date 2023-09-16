using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.VersionControl;
using UnityEngine;

//dit is een class om de command pattern uit te voeren. de command pattern heeft altijd: een client, concrete command, abstract command, invoker en receiver.
public interface ICommand
{
    void Execute();
}
public interface IGameObjectCommand
{
    //opzetten van de concrete command
    void Execute();
}
public class InputHandler
{
    private IGameObjectCommand Ycommand;
    private IGameObjectCommand Xcommand;
    public IGameObjectCommand Acommand;
    public IGameObjectCommand Bcommand;

    public InputHandler(IGameObjectCommand XButtonCommand, IGameObjectCommand YButtonCommand)
    {
        this.Xcommand = XButtonCommand;
        this.Ycommand = YButtonCommand;
    }

    public IGameObjectCommand HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Y)) { return Ycommand; }
        if (Input.GetKeyDown(KeyCode.X)) { return Xcommand; }
        // if (Input.GetKeyDown(KeyCode.A)) { Acommand.Execute(); };
        // if (Input.GetKeyDown(KeyCode.B)) { Bcommand.Execute(); };
        return null;
    }
}


//concrete command
public class FireGunCommand : IGameObjectCommand
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

public class TestMessage : IGameObjectCommand
{
    public void Execute()
    {
        Message();

    }

    public void Message()
    {
        Debug.Log("test message");
    }
}

public class Test2 : MonoBehaviour
{
    public InputHandler inputHandler = new InputHandler(new FireGunCommand(), new TestMessage());
    public IGameObjectCommand command;
    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        
        command = inputHandler.HandleInput();
    }

    // Update is called once per frame
    void Update()
    {
        command.Execute();
    }
}
