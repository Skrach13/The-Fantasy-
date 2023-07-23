using System.Collections.Generic;
using UnityEngine;

public class EnemiesGroupInMap : MonoBehaviour
{
    [SerializeField] private Vector2 _positionInGraf;
    [SerializeField] private GlobalMapGraf _mapGraf;
    [SerializeField] private int _countRangeWalk;
    [SerializeField] private EnemyProperties[] _enemies;

    private List<Vector2> _randomGrafWalk;
    private void Start()
    {
        transform.position = _mapGraf.Cells[(int)_positionInGraf.x, (int)_positionInGraf.y].transform.position;

    }

    private List<Vector2> AddAreaWalk()
    {
        List<Vector2> list = new List<Vector2>();

        for(int i = 0; i < _countRangeWalk; i++)
        {

        }
        
        return list;
    }

    private static List<CellBase> GetNeighboursCell(CellBase cellFloor, CellBase[,] field, int countRangeAttack)
    {
        var result = new List<CellBase>();
        // —оседними точками €вл€ютс€ соседние по стороне клетки.
        Vector2 position = cellFloor.PositiongCell;
        Vector2[] neighbourPoints = new Vector2[6];
        if (cellFloor.PositiongCell.y % 2 != 0)
        {
            neighbourPoints[0] = new Vector2(position.x - 1, position.y);//left
            neighbourPoints[1] = new Vector2(position.x - 1, position.y - 1);//left down
            neighbourPoints[2] = new Vector2(position.x, position.y - 1);//down right
            neighbourPoints[3] = new Vector2(position.x + 1, position.y);//right
            neighbourPoints[4] = new Vector2(position.x, position.y + 1); // up right
            neighbourPoints[5] = new Vector2(position.x - 1, position.y + 1);// up left 
        }
        else
        {
            neighbourPoints[0] = new Vector2(position.x - 1, position.y);//left
            neighbourPoints[1] = new Vector2(position.x, position.y - 1);//down left
            neighbourPoints[2] = new Vector2(position.x + 1, position.y - 1);//down right
            neighbourPoints[3] = new Vector2(position.x + 1, position.y);//right
            neighbourPoints[4] = new Vector2(position.x + 1, position.y + 1);//up right
            neighbourPoints[5] = new Vector2(position.x, position.y + 1);//up left
        }

        int countPoint = 0;

        foreach (var point in neighbourPoints)
        {


            // ѕровер€ем, что не вышли за границы карты.
            if (point.x < 0 || point.x >= field.GetLength(0))
                continue;
            if (point.y < 0 || point.y >= field.GetLength(1))
                continue;
            //if (!(field[(int)point.x, (int)point.y].AttackRange == 0))
            //    continue;

            //field[(int)point.x, (int)point.y].AttackRange = countRangeAttack;
            // ѕровер€ем, что по клетке можно ходить.
            //  if ((field[(int)point.x, (int)point.y] != 0) && (field[(int)point.x, (int)point.y] != 1))
            //     continue;

            if (countRangeAttack == 1)
            {
                field[(int)point.x, (int)point.y].PaintCell(Color.yellow);
            }
            else if (countRangeAttack == 2)
            {
                field[(int)point.x, (int)point.y].PaintCell(Color.blue);
            }
            else
            {
                field[(int)point.x, (int)point.y].PaintCell(Color.cyan);
            }

            countPoint++;
            result.Add(field[(int)point.x, (int)point.y]);
        }
        return result;
    }
}
