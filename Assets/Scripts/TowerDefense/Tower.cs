using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Tower", menuName = "TowerDefense/Tower")]
public class Tower : ScriptableObject
{
    public int id;
    public List<Sprite> sprites = new();
    public List<int> damages = new();
    // Rate of fire ...
    public Sprite bullet;
}
