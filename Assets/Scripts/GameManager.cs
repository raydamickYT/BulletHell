using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public event System.Action UpdateBullet;

    FSM<GameManager> fsm;
    public InputHandler inputHandler;
    public InstantiateGameObjects instantiateGameObjects;
    //public ICommand command;
    public GameObject PreFab;
    public Bullets bullets;
    public Player player;

    public Dictionary<string, GameObject> PrefabLibrary = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> InstantiatedObjects = new Dictionary<string, GameObject>();

    //alle variabelen voor mijn simpele object pool
    public List<GameObject> InactivePooledObjects = new List<GameObject>();
    public List<GameObject> ActivePooledObjects = new List<GameObject>();

    public int AmountToPool = 30;

    private void Awake()
    {
        fsm = new FSM<GameManager>();
        fsm.Initialize(this);
        fsm.AddState(new InstantiateGameObjects(fsm));

        fsm.SwitchState(typeof(InstantiateGameObjects));
    }

    // Start is called before the first frame update
    void Start()
    {
        //setup de input handler op een game object.
        inputHandler = new InputHandler();
        //door een var aan te maken voor een van de acties, hoef je niet de hele tijd 'new firefuncommand()" te schrijven en kan je meerdere knoppen voor een action gebruiken.
        var firegun = new FireGunCommand(this);
        //deze line zorgt ervoor dat er een key wordt gebind aan de juiste actie. de actie zit op de actor
        inputHandler.BindInputToCommand(KeyCode.X, firegun);
        inputHandler.BindInputToCommand(KeyCode.Y, firegun);
        inputHandler.BindInputToCommand(KeyCode.R, new TestMessage(this));
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(GameObjectsInScene.Count);
        inputHandler.HandleInput();
        fsm.Update();
        UpdateBullet?.Invoke();
    }

    public GameObject GetPooledObjects()
    {
        if (InactivePooledObjects.Count > 0)
        {
            if (!InactivePooledObjects[0].activeInHierarchy && FireGunCommand._canFire)
            {
                GameObject _object = InactivePooledObjects[0];
                ActivePooledObjects.Add(_object);
                InactivePooledObjects.Remove(_object);
                return _object;
            }
        }
        else
        {
            Debug.Log("no more bullets");
        }
        return null;
    }

    public void DeActivate(GameObject bullet)
    {
        if (ActivePooledObjects.Contains(bullet))
        {
            ActivePooledObjects.Remove(bullet);
            InactivePooledObjects.Add(bullet);
            bullet.SetActive(false);
        }
    }
    public void RunCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}
