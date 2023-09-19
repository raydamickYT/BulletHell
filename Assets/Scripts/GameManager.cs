using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public InputHandler inputHandler;
    public InstantiateGameObjects instantiateGameObjects;
    //public ICommand command;
    public GameObject PreFab;
    public Dictionary<string, GameObject> PrefabLibrary = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> InstantiadedObjects = new Dictionary<string, GameObject>();



    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null){
            Instance = this;
        }
        
        //setup de input handler op een game object.
        inputHandler = new InputHandler();
        instantiateGameObjects = new InstantiateGameObjects(this);

        //door een var aan te maken voor een van de acties, hoef je niet de hele tijd 'new firefuncommand()" te schrijven en kan je meerdere knoppen voor een action gebruiken.
        var firegun = new FireGunCommand();
        //deze line zorgt ervoor dat er een key wordt gebind aan de juiste actie. de actie zit op de actor
        inputHandler.BindInputToCommand(KeyCode.X, firegun);
        inputHandler.BindInputToCommand(KeyCode.Y, firegun);
        inputHandler.BindInputToCommand(KeyCode.R, new TestMessage(this));

        instantiateGameObjects.InstantiateObjects();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameObjectsInScene.Count);
        inputHandler.HandleInput();
    }
}
