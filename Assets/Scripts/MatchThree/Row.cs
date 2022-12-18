using UnityEngine;

public class Row : MonoBehaviour
{
    public Cell[] cells;

    public void SpawnCells(int n, GameObject cell)
    {
        cells = new Cell[n];
        
        for (var i = 0; i < n; i++)
        {
            var cellGO = Instantiate(cell, transform);
            cells[i] = cellGO.GetComponent<Cell>();
        }
    }
}
