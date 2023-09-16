using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Test : MonoBehaviour
{
    public InputHandler inputHandler;
    public IGameObjectCommand command;
    public GameObject test;

    // Start is called before the first frame update
    void Start()
    {
        inputHandler = new InputHandler(new FireGunCommand(), new TestMessage());
    }

    // Update is called once per frame
    void Update()
    {
        command = inputHandler.HandleInput();

        if (command != null)
        {
            command.Execute();
        }
    }
}
