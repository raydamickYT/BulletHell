using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.VersionControl;
using UnityEngine;
using System;

//dit is een class om de command pattern uit te voeren. de command pattern heeft altijd: een client, concrete command, abstract command, invoker en receiver.
public interface ICommand
{
    void Execute();
}
public class InputHandler
{
    private List<KeyCommand> keyCommands = new List<KeyCommand>();

    public void HandleInput()
    {
        foreach (var keyCommand in keyCommands)
        {
            if (Input.GetKeyDown(keyCommand.key))
            {
                keyCommand.command.Execute();
            }
        }
    }


    //deze snap ik niet helemaal, vraag even in de les voor uitleg
    public void BindInputToCommand(KeyCode keyCode, ICommand command)
    {
        keyCommands.Add(new KeyCommand()
        {
            key = keyCode,
            command = command
        });
    }

    public void UnBindInput(KeyCode keyCode)
    {
        var items = keyCommands.FindAll(x => x.key == keyCode);
        items.ForEach(x => keyCommands.Remove(x));
    }
}

public class KeyCommand
{
    public KeyCode key;
    public ICommand command;
}




