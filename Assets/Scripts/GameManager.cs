using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    FSM<GameManager> fsm;
    #region Adjustable Variables
    public int AmountToPool = 30;
    #endregion

    #region Delegates
    //Delegates
    public delegate void Deactivationhandler(GameObject bullet);
    public Deactivationhandler DeactivationDelegate;
    public delegate GameObject ObjectPoolDelegate();
    public ObjectPoolDelegate objectPoolDelegate;
    #endregion

    public InputHandler inputHandler;
    public ObjectPool objectPool;
    public PlayerMovement playerMovement;
    public GameObject PreFab;
    //scriptable object
    public Bullets bullets;
    #region Dictionaries and Lists
    public Dictionary<string, GameObject> PrefabLibrary = new Dictionary<string, GameObject>();
    public Dictionary<string, GameObject> InstantiatedObjects = new Dictionary<string, GameObject>();

    //alle dictionaries voor mijn simpele object pool
    public List<GameObject> InactivePooledObjects = new List<GameObject>();
    public List<GameObject> ActivePooledObjects = new List<GameObject>();
    #endregion


    private void Awake()
    {
        //ZORG DAT DIT BOVENAAN STAAT ANDERS KRIJG JE EEN NULLREFERENCE
        fsm = new FSM<GameManager>();
        fsm.Initialize(this);
        //setup voor de dependency injections (behalve bij de inputhandler)
        playerMovement = new PlayerMovement(fsm);
        inputHandler = new InputHandler();
        objectPool = new ObjectPool(this);
        //door een var aan te maken voor een van de acties, hoef je niet de hele tijd 'new firefuncommand()" te schrijven en kan je meerdere knoppen voor een action gebruiken.
        var fireGun = new FireGunCommand(fsm);
        //setup voor de finite state machine (fsm).
        //deze fsm is super overkill voor dit project en wordt eigenlijk amper gebruikt.
        fsm.AddState(new InstantiateGameObjects(fsm));
        fsm.AddState(fireGun);
        fsm.AddState(new IdleState(fsm));
        fsm.AddState(playerMovement);

        fsm.SwitchState(typeof(InstantiateGameObjects));


        //deze line zorgt ervoor dat er een key wordt gebind aan de juiste actie. de actie zit op de actor
        inputHandler.BindInputToCommand(KeyCode.X, fireGun, new MovementContext { Direction = Vector3.up });
        inputHandler.BindInputToCommand(KeyCode.Y, fireGun, new MovementContext { Direction = Vector3.up });
        inputHandler.BindInputToCommand(KeyCode.W, playerMovement, new MovementContext { Direction = Vector3.up });
        inputHandler.BindInputToCommand(KeyCode.A, playerMovement, new MovementContext { Direction = Vector3.left });
        inputHandler.BindInputToCommand(KeyCode.S, playerMovement, new MovementContext { Direction = Vector3.down });
        inputHandler.BindInputToCommand(KeyCode.D, playerMovement, new MovementContext { Direction = Vector3.right });



        //subscribe wat functies aan de delegates (beetje nutteloos, haal niks uit de delegates)
        DeactivationDelegate += DeActivate;

        objectPoolDelegate += objectPool.GetPooledObjects;
    }

    // Update is called once per frame
    void Update()
    {
        inputHandler.HandleInput();
        fsm.Update();
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

    //de functie die alle bullets van de active pool naar de inactive pool verplaatst.
    public void DeActivate(GameObject bullet)
    {
        if (ActivePooledObjects.Contains(bullet))
        {
            ActivePooledObjects.Remove(bullet);
            InactivePooledObjects.Add(bullet);
            bullet.SetActive(false);
        }
    }

    //omdat ik maar een monobehaviour gebruik en coroutines alleen gestart kunnen worden in een monobehaviour, voer ik hier alle coroutines uit.
    public void RunCoroutine(IEnumerator routine)
    {
        StartCoroutine(routine);
    }
}
