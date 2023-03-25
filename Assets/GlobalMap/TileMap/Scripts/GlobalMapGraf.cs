using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GlobalMapGraf : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private TestCellGrid[,] _cellsMap;
    // Start is called before the first frame update
    void Start()
    {
        _cellsMap = CreateGraff(_tilemap);

    }

    private TestCellGrid[,] CreateGraff(Tilemap tilemap)
    {
        _tilemap.CompressBounds();
        int tileSizeX = _tilemap.size.x;
        int tileSizeY = _tilemap.size.y;

        TestCellGrid[,] cellsMap = new TestCellGrid[tileSizeX, tileSizeY];
        int countX = 0;
        int countY = 0;
        for (int y = _tilemap.cellBounds.yMin; y <= tilemap.cellBounds.yMax; y++)
        {
            for (int x = _tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++)
            {
                if (tilemap.GetInstantiatedObject(new Vector3Int(x, y)) != null)
                {
                    cellsMap[countX, countY] = tilemap!.GetInstantiatedObject(new Vector3Int(x, y)).GetComponent<TestCellGrid>();
                }
              countX++;
            }
          countX = 0;
          countY++;
        }
        return cellsMap;
    }
}
