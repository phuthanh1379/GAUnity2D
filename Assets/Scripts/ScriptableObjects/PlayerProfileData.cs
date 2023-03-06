using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerProfileData", menuName = "PlayerProfile")]
public class PlayerProfileData : ScriptableObject
{
    public string playerName;
    public int playerAge;
    public int playerScore;
    public List<int> sceneIndexes = new();
    public PlayerProfileExample example;
}

[Serializable]
public class PlayerProfileExample
{
    public int TotalGold;
    public int MaxScore;
    public string LastName;
}
