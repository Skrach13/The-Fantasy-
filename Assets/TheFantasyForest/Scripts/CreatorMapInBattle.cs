using UnityEngine;
using UnityEngine.Tilemaps;

public class CreatorMapInBattle : MonoBehaviour
{
    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private CellInBattle[,] _cellsMap;
    [SerializeField] private GrafMapInBatle _battleMapGraf;
    void Awake()
    {
        _cellsMap = CreateGraff(_tilemap);
        _battleMapGraf.Cells = _cellsMap;

    }
    private void Start()
    {
        _battleMapGraf.AddListner();
    }

    private CellInBattle[,] CreateGraff(Tilemap tilemap)
    {
        _tilemap.CompressBounds();
        int tileSizeX = _tilemap.size.x;
        int tileSizeY = _tilemap.size.y;

        CellInBattle[,] cellsMap = new CellInBattle[tileSizeX, tileSizeY];
        int countX = 0;
        int countY = 0;
        for (int y = _tilemap.cellBounds.yMin; y <= tilemap.cellBounds.yMax; y++)
        {
            for (int x = _tilemap.cellBounds.xMin; x <= tilemap.cellBounds.xMax; x++)
            {
                if (tilemap.GetInstantiatedObject(new Vector3Int(x, y)) != null)
                {
                    var cell = cellsMap[countX, countY] = tilemap!.GetInstantiatedObject(new Vector3Int(x, y)).GetComponent<CellInBattle>();
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
