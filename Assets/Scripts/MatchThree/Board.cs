using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class Board : MonoBehaviour
{
    public static Board Instance;

    [Header("Board Settings")] 
    public GameObject cellPrefab;
    public GameObject rowPrefab;
    public int width;
    public int height;
    public float tweenDuration = 0.5f;
    public Row[] rows;
    public Cell[,] Cells;
    
    public int Width => Cells.GetLength(0);
    public int Height => Cells.GetLength(1);

    private List<Cell> _selectedCells = new();
    
    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip clip;

    private bool _isSelectable;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Init();
    }

    // private void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.A))
    //     {
    //         foreach (var connectedCell in Cells[0, 0].GetConnectedCells())
    //             connectedCell.image.transform.DOScale(1.25f, tweenDuration).Play();
    //     }
    // }

    private void Init()
    {
        // Set variables
        _isSelectable = true;
        
        // Create board
        // Cells = new Cell[rows.Max(row => row.cells.Length), rows.Length];
        Cells = new Cell[width, height];
        rows = new Row[height];
        
        for (var y = 0; y < height; y++)
        {
            var rowGO = Instantiate(rowPrefab, transform);
            var row = rowGO.GetComponent<Row>();
            row.SpawnCells(width, cellPrefab);
            rows[y] = row;
        }
        
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var cell = rows[y].cells[x];

                cell.x = x;
                cell.y = y;

                cell.Data = CellDatabase.Instance.GetNewData();
                Cells[x, y] = cell;
                Cells[x, y].Init();
            }
        }

        // Not recommended ...
        Pop();
    }

    public async void Select(Cell cell)
    {
        if (!_isSelectable) return;
        if (!_selectedCells.Contains(cell))
        {
            if (_selectedCells.Count > 0)
            {
                if (Array.IndexOf(_selectedCells[0].Neighbours, cell) != -1)
                {
                    cell.OnSelect();
                    _selectedCells.Add(cell);
                }
            }
            else
            {
                cell.OnSelect();
                _selectedCells.Add(cell);
            }
        }
     
        if (_selectedCells.Count < 2) return;

        await SwapCells(_selectedCells[0], _selectedCells[1]);
        
        _selectedCells[0].OnDeselect();
        _selectedCells[1].OnDeselect();
        
        // Check if match
        if (CanPop())
        {
            Pop();
        }
        else
        {
            await SwapCells(_selectedCells[0], _selectedCells[1]);
        }
        
        _selectedCells.Clear();
    }

    private async Task SwapCells(Cell cell1, Cell cell2)
    {
        _isSelectable = false;
        var sequence = DOTween.Sequence();
        
        var img1 = cell1.image;
        var img2 = cell2.image;

        var img1Transform = img1.transform;
        var img2Transform = img2.transform;

        sequence
            .Join(img1Transform.DOMove(img2Transform.position, tweenDuration))
            .Join(img2Transform.DOMove(img1Transform.position, tweenDuration))
            ;

        await sequence.Play()
            .AsyncWaitForCompletion();
        
        // After sequence is completed, swap 2 cells' images
        img1Transform.SetParent(cell2.transform);
        img2Transform.SetParent(cell1.transform);

        cell1.image = img2;
        cell2.image = img1;
        
        // ... Then swap 2 cells' data
        var cell1Data = cell1.Data;

        cell1.Data = cell2.Data;
        cell2.Data = cell1Data;
        
        // Make selectable
        _isSelectable = true;
    }

    private bool CanPop()
    {
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                if (Cells[x, y].GetConnectedCells().Skip(1).Count() >= 2)
                    return true;
            }
        }

        return false;
    }

    private async void Pop()
    {
        _isSelectable = false;
        for (var y = 0; y < height; y++)
        {
            for (var x = 0; x < width; x++)
            {
                var cell = Cells[x, y];

                var connectedCells = cell.GetConnectedCells();

                if (connectedCells.Skip(1).Count() < 2) continue;

                var deflateSequence = DOTween.Sequence();

                foreach (var connectedCell in connectedCells)   
                {
                    deflateSequence.Join(connectedCell.image.transform.DOScale(Vector3.zero, tweenDuration));
                }

                await deflateSequence
                    .Play()
                    .AsyncWaitForCompletion();
                
                // Play sfx
                audioSource.PlayOneShot(clip);

                // Calculate score
                Match3Score.Instance.Score += cell.Data.value * connectedCells.Count;

                var inflateSequence = DOTween.Sequence();
                
                foreach (var connectedCell in connectedCells)
                {
                    connectedCell.Data = CellDatabase.Instance.GetNewData();
                    inflateSequence.Join(connectedCell.image.transform.DOScale(Vector3.one, tweenDuration));
                }

                await inflateSequence
                    .Play()
                    .AsyncWaitForCompletion();

                x = 0;
                y = 0;
            }
        }

        _isSelectable = true;
    }
}
