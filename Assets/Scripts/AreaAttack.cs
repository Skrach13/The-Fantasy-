using Mono.Collections.Generic;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static AreaAttack;
using static PathFinder;

public class AreaAttack : MonoBehaviour
{
    public static void AttackArea(CellFloorScripts[,] fields, APersoneScripts attacking)
    {
        Vector2 vector = attacking.battlePosition;//позици€ атакующего
        int counAtattackRange = 1;// счетчик дистанции атаки оружи€
        fields[(int)vector.x, (int)vector.y].attackRange = attacking.rangeWeapone;// присваивание €чейки где стоит атакующей дистанции оружи€ дл€ закрытие €чейки
        //колекци€ €чеек попадающих в дистанцую атаки
        Collection<CellFloorScripts> fistCollection = GetNeighbours((fields[(int)attacking.battlePosition.x, (int)attacking.battlePosition.y]), fields , counAtattackRange); ;
        Collection<CellFloorScripts> secondCollection = new Collection<CellFloorScripts>();
        counAtattackRange++;
        while (counAtattackRange <= attacking.rangeWeapone)
        {
            secondCollection.Clear();
            var collection = new Collection<CellFloorScripts>();
            foreach (CellFloorScripts cell in fistCollection)
            {
                collection = GetNeighbours(cell, fields, counAtattackRange);
                secondCollection.AddRange(collection);
                Debug.Log(cell.positiongGrafCellField);

            }
            fistCollection.Clear();
            fistCollection.AddRange(secondCollection);
            counAtattackRange++;
        }

    } 
    
    /// <summary>
    /// костыль дл€ ј»
    /// </summary>
    /// <param name="fields"></param>
    /// <param name="attacking"></param>
    public static List<APersoneScripts> PersoneAttackArea(CellFloorScripts[,] fields, APersoneScripts attacking)
    {
        List<APersoneScripts> playerPersone = new List<APersoneScripts>();
        Vector2 vector = attacking.battlePosition;//позици€ атакующего
        int counAtattackRange = 1;// счетчик дистанции атаки оружи€
        fields[(int)vector.x, (int)vector.y].attackRange = attacking.rangeWeapone;// присваивание €чейки где стоит атакующей дистанции оружи€ дл€ закрытие €чейки
        //колекци€ €чеек попадающих в дистанцую атаки
        Collection<CellFloorScripts> fistCollection = GetNeighbours((fields[(int)attacking.battlePosition.x, (int)attacking.battlePosition.y]), fields , counAtattackRange); ;
        Collection<CellFloorScripts> secondCollection = new Collection<CellFloorScripts>();
        counAtattackRange++;
        while (counAtattackRange <= attacking.rangeWeapone)
        {
            secondCollection.Clear();
            var collection = new Collection<CellFloorScripts>();
            foreach (CellFloorScripts cell in fistCollection)
            {
                if (cell.closeCell)
                {
                    if (fields[(int)cell.positiongGrafCellField.x, (int)cell.positiongGrafCellField.y].personeStayInCell.personeType == APersoneScripts.PersoneType.Player)
                    {
                        playerPersone.Add(fields[(int)cell.positiongGrafCellField.x, (int)cell.positiongGrafCellField.y].personeStayInCell);
                    }
                }
                collection = GetNeighbours(cell, fields, counAtattackRange);
                secondCollection.AddRange(collection);
                Debug.Log(cell.positiongGrafCellField);

            }
            fistCollection.Clear();
            fistCollection.AddRange(secondCollection);
            counAtattackRange++;
        }
        return playerPersone;

    }

    private static Collection<CellFloorScripts> GetNeighbours(CellFloorScripts cellFloor, CellFloorScripts[,] field,int countRangeAttack )
    {
        var result = new Collection<CellFloorScripts>();
        // —оседними точками €вл€ютс€ соседние по стороне клетки.
        Vector2 position = cellFloor.positiongGrafCellField;
        Vector2[] neighbourPoints = new Vector2[6];
        if (cellFloor.positiongGrafCellField.y % 2 == 0)
        {
            neighbourPoints[0] = new Vector2(position.x - 1, position.y);//left
            neighbourPoints[1] = new Vector2(position.x - 1, position.y - 1);//left down
            neighbourPoints[2] = new Vector2(position.x,position.y - 1);//down right
            neighbourPoints[3] = new Vector2(position.x + 1, position.y);//right
            neighbourPoints[4] = new Vector2(position.x, position.y + 1); // up right
            neighbourPoints[5] = new Vector2(position.x - 1, position.y + 1);// up left 
        }
        else
        {
            neighbourPoints[0] = new Vector2(position.x - 1,position.y);//left
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
            if (!(field[(int)point.x, (int)point.y].attackRange == 0))
                continue;

            field[(int)point.x, (int)point.y].attackRange = countRangeAttack;
            // ѕровер€ем, что по клетке можно ходить.
            //  if ((field[(int)point.x, (int)point.y] != 0) && (field[(int)point.x, (int)point.y] != 1))
            //     continue;

            if (countRangeAttack == 1)
            {
            field[(int)point.x, (int)point.y].paintCellBattle(Color.yellow);
            }else if(countRangeAttack == 2)
            {
                field[(int)point.x, (int)point.y].paintCellBattle(Color.blue);
            }
            else
            {
                field[(int)point.x, (int)point.y].paintCellBattle(Color.red);
            }


            if(field[(int)point.x, (int)point.y].personeStayInCell != null)
            {
                field[(int)point.x, (int)point.y].paintCellBattle(Color.red);
            }
            //point.paintCellBattle(Color.red);



            countPoint++;
            result.Add(field[(int)point.x, (int)point.y]);
        }
        return result;
    }

  
    
}
