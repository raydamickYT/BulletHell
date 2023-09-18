using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameManager Instance;
    public InputHandler inputHandler;
    //public ICommand command;
    public GameObject PreFab;
    static public Dictionary<string, GameObject> GameObjectsInScene = new Dictionary<string, GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null){
            Instance = this;
        }
        
        //setup de input handler op een game object.
        inputHandler = new InputHandler();
        //door een var aan te maken voor een van de acties, hoef je niet de hele tijd 'new firefuncommand()" te schrijven en kan je meerdere knoppen voor een action gebruiken.
        var firegun = new FireGunCommand();
        //deze line zorgt ervoor dat er een key wordt gebind aan de juiste actie. de actie zit op de actor
        inputHandler.BindInputToCommand(KeyCode.X, firegun);
        inputHandler.BindInputToCommand(KeyCode.Y, firegun);
        inputHandler.BindInputToCommand(KeyCode.R, new TestMessage());

        //voeg gameobjecten toe aan je dictionary
        GameObjectsInScene.Add("player", PreFab);
        GameObjectsInScene.Add("enemy", PreFab);

        //instantiaten van alle objecten in de dictionary
        foreach (var kvp in GameObjectsInScene)
        {
            GameObject instantiatedObject = Instantiate(kvp.Value);
            instantiatedObject.name = kvp.Key; // optioneel, maar netjes: verander de naam van het gameobject in de wereld naar de kvp waarde(string)
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameObjectsInScene.Count);
        inputHandler.HandleInput();
    }
}
