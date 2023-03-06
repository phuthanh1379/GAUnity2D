using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TDManager : MonoBehaviour
{
    public Tilemap tilemap;
    public Transform test;
    public Transform towersHolder;    
    
    public List<GameObject> towers = new();
    public List<GameObject> towersPrefabs = new();

    private int _selectedTowerID;
    
    private void Update()
    {
        DetectSpawnPoint();
    }

    private void DetectSpawnPoint()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main is not null)
            {
                var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                var cellPosRaw = tilemap.WorldToCell(mousePos);
                var cellPosCentered = tilemap.GetCellCenterWorld(cellPosRaw);
            
                // Check if can spawn
                if (tilemap.GetColliderType(cellPosRaw) == Tile.ColliderType.Sprite)
                {
                    // Spawn tower
                    test.transform.position = cellPosCentered;

                    // Disable the collider
                    tilemap.SetColliderType(cellPosRaw, Tile.ColliderType.None);
                }
                else
                {
                    tilemap.SetColliderType(cellPosRaw, Tile.ColliderType.Sprite);
                }
            }
        }
    }

    private void SpawnTower(Vector3 spawnPosition)
    {
        var tower = Instantiate(towersPrefabs[_selectedTowerID], towersHolder);
    }
    
}
