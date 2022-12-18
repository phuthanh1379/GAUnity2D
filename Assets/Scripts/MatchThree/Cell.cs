using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int x;
    public int y;

    private CellData _data;
    public CellData Data
    {
        get => _data;
        set
        {
            if (_data == value) return;

            _data = value;

            image.sprite = _data.sprite;
        }
    }
    public Image image;
    public Button button;
    public GameObject frame;
    public Canvas canvas;

    public Cell Left => x > 0 ? Board.Instance.Cells[x - 1, y] : null;
    public Cell Right => x < Board.Instance.Width - 1 ? Board.Instance.Cells[x + 1, y] : null;
    public Cell Top => y > 0 ? Board.Instance.Cells[x, y - 1] : null;
    public Cell Bottom => y < Board.Instance.Height - 1 ? Board.Instance.Cells[x, y + 1] : null;

    public Cell[] Neighbours => new[]
    {
        Left,
        Right,
        Top,
        Bottom
    };

    public void Init()
    {
        button.onClick.AddListener(() => Board.Instance.Select(this));
    }

    public List<Cell> GetConnectedCells(List<Cell> exclude = null)
    {
        var result = new List<Cell> {this,};
        if (exclude == null)
        {
            exclude = new List<Cell> {this,};
        }
        else
        {
            exclude.Add(this);
        }

        foreach (var neighbour in Neighbours)
        {
            if (neighbour == null || exclude.Contains(neighbour) || neighbour.Data != Data) continue;
            
            result.AddRange(neighbour.GetConnectedCells(exclude));
        }

        return result;
    }

    public void OnSelect()
    {
        Scale(1.1f);
        frame.SetActive(true);
        canvas.sortingOrder = 2;
    }

    public void OnDeselect()
    {
        Scale(1f);
        frame.SetActive(false);
        canvas.sortingOrder = 1;
    }
    
    private void Scale(float mult)
    {
        transform.localScale = Vector3.one * mult;
    }
}
