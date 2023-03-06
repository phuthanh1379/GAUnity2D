using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowersDatabase : MonoBehaviour
{
    public static TowersDatabase Instance;
    public List<Tower> tower = new();
    public List<Image> towerImages = new();

    private void Init()
    {
        
    }
}
