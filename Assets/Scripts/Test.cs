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
        command = inputHandler.HandleInput();
    }

    // Update is called once per frame
    void Update()
    {
        //command.Execute();
        Debug.Log(command);
    }
}
