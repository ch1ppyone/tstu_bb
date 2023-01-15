using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class Wave
{
    public List<GameObject> content;
}


[CreateAssetMenu(fileName = "GameData", menuName = "Game/Game")]
public class GameData :  ScriptableObject
{
    public List <Wave> waves;
}
