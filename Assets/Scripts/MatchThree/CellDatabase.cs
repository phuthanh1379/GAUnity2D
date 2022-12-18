using System.Collections.Generic;
using UnityEngine;

public class CellDatabase : MonoBehaviour
{
    public static CellDatabase Instance;

    [SerializeField] private List<CellData> cellData = new();

    private void Awake()
    {
        Instance = this;
    }

    public CellData GetNewData()
    {
        var rnd = new System.Random();
        return cellData[rnd.Next(cellData.Count)];
    }
}
