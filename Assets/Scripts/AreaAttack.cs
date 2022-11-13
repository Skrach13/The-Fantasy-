using Mono.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static AreaAttack;
using static PathFinder;

public class AreaAttack : MonoBehaviour
{
    public static void AttackArea(CellFloorScripts[,] fields, APersoneScripts attacking)
    {
        Vector2 vector = attacking.battlePosition;
        int counAtattackRange = attacking.rangeWeapone;
        Collection<CellFloorScripts> fistCollection = GetNeighbours((fields[(int)attacking.battlePosition.x, (int)attacking.battlePosition.y]), fields); ;
        Collection<CellFloorScripts> secondCollection = new Collection<CellFloorScripts>();
        counAtattackRange--;
        while (counAtattackRange > 0)
        {
            foreach (var cell in fistCollection)
            {
                secondCollection= GetNeighbours(cell, fields);
            }
            // fistCollection = secondCollection + secondCollection;

            counAtattackRange--;
        }

    }

    private static Collection<CellFloorScripts> GetNeighbours(CellFloorScripts cellFloor, CellFloorScripts[,] field )
    {
        var result = new Collection<CellFloorScripts>();
        // —оседними точками €вл€ютс€ соседние по стороне клетки.
        CellFloorScripts[] neighbourPoints = new CellFloorScripts[6];
        if (cellFloor.positiongGrafCellField.y % 2 == 0)
        {
            neighbourPoints[0] = field[(int)cellFloor.positiongGrafCellField.x - 1, (int)cellFloor.positiongGrafCellField.y];//left
            neighbourPoints[1] = field[(int)cellFloor.positiongGrafCellField.x - 1, (int)cellFloor.positiongGrafCellField.y - 1];//left down
            neighbourPoints[2] = field[(int)cellFloor.positiongGrafCellField.x, (int)cellFloor.positiongGrafCellField.y - 1];//down right
            neighbourPoints[3] = field[(int)cellFloor.positiongGrafCellField.x + 1, (int)cellFloor.positiongGrafCellField.y];//right
            neighbourPoints[4] = field[(int)cellFloor.positiongGrafCellField.x, (int)cellFloor.positiongGrafCellField.y + 1]; // up right
            neighbourPoints[5] = field[(int)cellFloor.positiongGrafCellField.x - 1, (int)cellFloor.positiongGrafCellField.y + 1];// up left 

        }
        else
        {
            neighbourPoints[0] = field[(int)cellFloor.positiongGrafCellField.x - 1, (int)cellFloor.positiongGrafCellField.y];//left
            neighbourPoints[1] = field[(int)cellFloor.positiongGrafCellField.x, (int)cellFloor.positiongGrafCellField.y - 1];//down left
            neighbourPoints[2] = field[(int)cellFloor.positiongGrafCellField.x + 1, (int)cellFloor.positiongGrafCellField.y - 1];//down right
            neighbourPoints[3] = field[(int)cellFloor.positiongGrafCellField.x + 1, (int)cellFloor.positiongGrafCellField.y];//right
            neighbourPoints[4] = field[(int)cellFloor.positiongGrafCellField.x + 1, (int)cellFloor.positiongGrafCellField.y + 1];//up right
            neighbourPoints[5] = field[(int)cellFloor.positiongGrafCellField.x, (int)cellFloor.positiongGrafCellField.y + 1];//up left
        }

        int countPoint = 0;

        foreach (var point in neighbourPoints)
        {


            // ѕровер€ем, что не вышли за границы карты.
            if (point.positiongGrafCellField.x < 0 || point.positiongGrafCellField.x >= field.GetLength(0))
                continue;
            if (point.positiongGrafCellField.y < 0 || point.positiongGrafCellField.y >= field.GetLength(1))
                continue;
            // ѕровер€ем, что по клетке можно ходить.
          //  if ((field[(int)point.x, (int)point.y] != 0) && (field[(int)point.x, (int)point.y] != 1))
          //     continue;
            point.paintCellBattle(Color.red);



            countPoint++;
            result.Add(point);
        }
        return result;
    }

  
    
}
