using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesGroupInMap : GroupInMap
{
    [SerializeField] private int _countRangeWalk;
    public EnemyProperties[] Enemies;
    private Vector2 _positionInMap;
    public bool IsBattle; 

    private List<GlobalMapCell> _randomGrafWalk;
    public List<GlobalMapCell> RandomGrafWalk { get => _randomGrafWalk; set => _randomGrafWalk = value; }
    public int CountRangeWalk { get => _countRangeWalk; set => _countRangeWalk = value; }
    public Vector2 PositionInMap { get => _positionInMap; set => _positionInMap = value; }

    private new void Start()
    {
        base.Start();
        MoveInMap = GetComponent<MoveInMap>();
        MoveInMap.PositionInMap = _positionInMap;
        _raycast.OnRaycastHit += CheckedAnotherGroup;
        RandomGrafWalk ??= AddAreaWalk();
        StartCoroutine(PatrolWalk());
    }
    private void OnDestroy()
    {
        _raycast.OnRaycastHit -= CheckedAnotherGroup;
    }

    private void CheckedAnotherGroup(GroupInMap group)
    {
        Debug.Log($"{group.name}");
        BattleData.Instance.StartBattle(Enemies);
    }

    private IEnumerator PatrolWalk()
    {
        while (true)
        {
            List<CellBase> path = new();
            foreach (var cellPosition in PathFinder.Path(GlobalMapGraf.Instance.Cells, MoveInMap.PositionInMap, RandomGrafWalk[Random.Range(0, RandomGrafWalk.Count)].PositionInGraff))
            {
                path.Add(GlobalMapGraf.Instance.Cells[(int)cellPosition.x, (int)cellPosition.y]);
            }
            yield return StartCoroutine(MoveInMap.MovedInPath(path));
        }
    }


    private List<GlobalMapCell> AddAreaWalk()
    {
        Vector2 position = MoveInMap.PositionInMap;
        List<GlobalMapCell> firstList = new();
        firstList = GetNeighboursCell(GlobalMapGraf.Instance.Cells[(int)position.x, (int)position.y], GlobalMapGraf.Instance.Cells, firstList);
        firstList.Add(GlobalMapGraf.Instance.Cells[(int)position.x, (int)position.y]);
        GlobalMapGraf.Instance.Cells[(int)position.x, (int)position.y].PaintCell(Color.blue);
        int count = 1;
        while (count < CountRangeWalk)
        {
            List<GlobalMapCell> nextList = new();
            for (int i = 0; i < firstList.Count; i++)
            {
                var List = GetNeighboursCell(firstList[i], GlobalMapGraf.Instance.Cells, firstList);
                foreach (GlobalMapCell Cell in List)
                {
                    nextList.Add(Cell);
                }
            }
            foreach (GlobalMapCell cell in nextList)
            {
                firstList.Add(cell);
            }
            count++;
        }
        return firstList;
    }

    private List<GlobalMapCell> GetNeighboursCell(GlobalMapCell cellFloor, GlobalMapCell[,] field, List<GlobalMapCell> firstList)
    {
        var result = new List<GlobalMapCell>();
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
