using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "Bullet", menuName = "ScriptableObjects/BulletsData", order = 1)]
public class Bullets : ScriptableObject
{
    public GameObject BulletObject;
}
[CreateAssetMenu(fileName = "Player", menuName = "ScriptableObjects/PlayersData", order = 2)]
public class Player : ScriptableObject
{
    public GameObject PlayerObject;
}
