// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System;

// public interface IPoolable
// {
//     GameObject GameObject { get; set; }
//     void onEnableObject();
//     void onDisableObject();
// }

// public class ObjectPool<gameobject> where gameobject : IPoolable
// {
//     private List<GameObject> _activePool = new List<GameObject>();
//     private List<GameObject> _inactivePool = new List<GameObject>();

//     //als deze functie wordt aangeroepen dan wordt er een nieuwe instance gemaakt van een aangegeven type (wordt bepaald als je de functie roept)
//     private GameObject AddNewItemToPool()
//     {
//         GameObject LastAdded = null;
//         foreach (var kvp in GameManager.GameObjectsInScene)
//         {
//             LastAdded = kvp.Value;
//             _inactivePool.Add(LastAdded);
//             LastAdded.gameObject.SetActive(false);
//         }
//         return LastAdded;
//     }

//     //als een item geactiveerd wordt, dan verwijderd deze functie de item van de pool en voegt hij hem toe aan de active pool
//     public gameobject ActivateItem(gameobject item)
//     {
//         item.onEnableObject();
//         item.GameObject.SetActive(true);
//         if (_inactivePool.Contains(item.GameObject))
//         {
//             _inactivePool.Remove(item.GameObject);

//         }
//         _activePool.Add(item.GameObject);

//         return item;
//     }

//     //als deze functie wordt gecalled, dan word er gecheckt of de item die is meegegeven in de activeobject pool zit.
//     // alls dat zo is dan wordt dat object verwijiderd uit de pool, gedeactiveerd en aan de inactive pool toegevoegd.
//     public void ReturnToObjectPool(T item)
//     {
//         if (_activePool.Contains(item))
//         {
//             _activePool.Remove(item);
//         }
//         item.onDisableObject();
//         item.Active = false;
//         _inactivePool.Add(item);
//     }

//     //als deze functie wordt gecalled, dan wordt er gecheckt of er een object in de _inactivepool zit. zo ja, dan wordt deze geactiveerd en toegevoegd aan de _active pool
//     // zo niet dan wordt er een nieuw item aangemaakt.
//     public T RequestObject()
//     {
//         if (_inactivePool.Count > 0)
//         {
//             //return ActivateItem(_inactivePool[0]);
//         }
//         return ActivateItem(AddNewItemToPool());
//     }


// }

// public class Pool : MonoBehaviour
// {
//     private ObjectPool<Enemy> _enemyPool;

//     private void Awake()
//     {
//         _enemyPool = new ObjectPool<Enemy>();
//     }


//     // Update is called once per frame
//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.Space))
//         {
//             Enemy enemy = _enemyPool.RequestObject();
//             //hier word de functie toegevoegd aan een event en dus als je de event invoked, wordt de functie ook uitgevoerd.
//             //vergeet dit niet weer te verwijderen als je het doet, anders gaat het voor error zorgen.
//             enemy.OnDie += OnEnemyDead;
//             /*

//                 use enemy here

//             */
//             enemy.Die();
//         }
//     }

//     //deze functie returned de enemy terug naar de inactive pool
//     public void OnEnemyDead(Enemy enemy)
//     {
//         _enemyPool.ReturnToObjectPool(enemy);
//     }
// }
