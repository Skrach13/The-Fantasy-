using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGroupInMap : MonoBehaviour
{
   // [SerializeField] private Vector2 _positionInGraf;
    [SerializeField] private GlobalMapGraf _mapGraf;
    [SerializeField] private int _countRangeWalk;
    [SerializeField] private EnemyProperties[] _enemies;
    [SerializeField] private MoveInMap _moveInMap;

    private List<CellBase> _randomGrafWalk;
    private void Start()
    {
        _moveInMap = GetComponent<MoveInMap>();
      //  transform.position = _mapGraf.Cells[(int)_positionInGraf.x, (int)_positionInGraf.y].transform.position;
        _randomGrafWalk = AddAreaWalk();
        StartCoroutine(PatrolWalk());
    }

    private IEnumerator PatrolWalk()
    {
        while (true)
        {
            List<CellBase> path = new List<CellBase>();
            foreach (var cellPosition in PathFinder.Path(_mapGraf.Cells, _moveInMap.PositionInMap, _randomGrafWalk[Random.Range(0, _randomGrafWalk.Count)].PositionInGraff))
            {
                path.Add(_mapGraf.Cells[(int)cellPosition.x, (int)cellPosition.y]);
            }
            yield return StartCoroutine( _moveInMap.MovedInPath(path));
        }

       //  yield break;
    }


    private List<CellBase> AddAreaWalk()
    {
        Vector2 position = _moveInMap.PositionInMap;
        List<CellBase> firstList = new List<CellBase>();
        firstList = GetNeighboursCell(_mapGraf.Cells[(int)position.x, (int)position.y], _mapGraf.Cells, firstList);
        firstList.Add(_mapGraf.Cells[(int)position.x, (int)position.y]);
        _mapGraf.Cells[(int)position.x, (int)position.y].PaintCell(Color.blue);
        int count = 1;
        while (count < _countRangeWalk)
        {
            Debug.Log($"{count}");
            List<CellBase> nextList = new List<CellBase>();
            for (int i = 0; i < firstList.Count; i++)
            {
                var List = GetNeighboursCell(firstList[i], _mapGraf.Cells, firstList);
                foreach (CellBase Cell in List)
                {
                    nextList.Add(Cell);
                }
            }
            foreach (CellBase cell in nextList)
            {
                firstList.Add(cell);
            }
            count++;
        }
        return firstList;
    }

    private List<CellBase> GetNeighboursCell(CellBase cellFloor, CellBase[,] field, List<CellBase> firstList)
    {
        var result = new List<CellBase>();
        // —оседними точками €вл€ютс€ соседние по стороне клетки.
        Vector2 position = cellFloor.PositionInGraff;
        Vector2[] neighbourPoints = new Vector2[6];
        if (cellFloor.PositionInGraff.y % 2 != 0)
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

            if (firstList.Contains(field[(int)point.x, (int)point.y]))
                continue;

            field[(int)point.x, (int)point.y].PaintCell(Color.blue);

            countPoint++;
            result.Add(field[(int)point.x, (int)point.y]);
        }
        return result;
    }
}
