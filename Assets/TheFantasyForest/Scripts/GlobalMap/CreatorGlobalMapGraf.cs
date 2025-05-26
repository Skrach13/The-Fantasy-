using UnityEngine;
using UnityEngine.Tilemaps;

public class CreatorGlobalMapGraf : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;   
    [SerializeField] private GlobalMapCell[,] _cellsMap;   
    [SerializeField] private GlobalMapGraf _globalMapGraf;   
    void Awake()
    {
        _cellsMap = CreateGraff(_tilemap);
        _globalMapGraf.Cells = _cellsMap;

    }
    private void Start()
    {
        _globalMapGraf.AddListner();
        
    }

    private GlobalMapCell[,] CreateGraff(Tilemap tilemap)
    {
        _tilemap.CompressBounds();
        int tileSizeX = _tilemap.size.x;
        int tileSizeY = _tilemap.size.y;

        GlobalMapCell[,] cellsMap = new GlobalMapCell[tileSizeX, tileSizeY];
        int countX = 0;
        int countY = 0;
        for (int y = _tilemap.cellBounds.yMin; y <= tilemap.cellBounds.yMax; y++)
        {
            for (int x = _tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++)
            {
                if (tilemap.GetInstantiatedObject(new Vector3Int(x, y)) != null)
                {
                    var cell = cellsMap[countX, countY] = tilemap!.GetInstantiatedObject(new Vector3Int(x, y)).GetComponent<GlobalMapCell>();
                    cell.PositionInGraff = new Vector2(countX, countY);
                }
              countX++;
            }
          countX = 0;
          countY++;
        }
        return cellsMap;
    }
}
