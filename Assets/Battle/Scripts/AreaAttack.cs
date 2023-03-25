using Mono.Collections.Generic;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnumInBattle;


public class AreaAttack : MonoBehaviour
{
    /// <summary>
    /// подкрашевание €чеек в радиусе атаки и подкраска €чеек красным цветом на которых стоит персонаж в радиусе атаки
    /// </summary>
    /// <param name="fields"></param>
    /// <param name="attacking"></param>
    public static void AttackArea(CellInBattle[,] fields, PersoneInBattle attacking)
    {
        Vector2 vector = attacking.battlePosition;//позици€ атакующего
        int counAtattackRange = 1;// счетчик дистанции атаки оружи€
        fields[(int)vector.x, (int)vector.y].AttackRange = attacking.rangeWeapone;// присваивание €чейки где стоит атакующей дистанции оружи€ дл€ закрытие €чейки
        //колекци€ €чеек попадающих в дистанцую атаки
        Collection<CellInBattle> fistCollection = GetNeighboursCell((fields[(int)attacking.battlePosition.x, (int)attacking.battlePosition.y]), fields, counAtattackRange); ;
        Collection<CellInBattle> secondCollection = new Collection<CellInBattle>();
        counAtattackRange++;
        while (counAtattackRange <= attacking.rangeWeapone)
        {
            secondCollection.Clear();
            var collection = new Collection<CellInBattle>();
            foreach (CellInBattle cell in fistCollection)
            {
                collection = GetNeighboursCell(cell, fields, counAtattackRange);
                secondCollection.AddRange(collection);              
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
    public static List<PersoneInBattle> PersoneAttackArea(CellInBattle[,] fields, PersoneInBattle attacking)
    {
        List<PersoneInBattle> playerPersone = new List<PersoneInBattle>();
        Vector2 vector = attacking.battlePosition;//позици€ атакующего
        int counAtattackRange = 1;// счетчик дистанции атаки оружи€
        fields[(int)vector.x, (int)vector.y].AttackRange = attacking.rangeWeapone;// присваивание €чейки где стоит атакующей дистанции оружи€ дл€ закрытие €чейки
        //колекци€ €чеек попадающих в дистанцую атаки
        Collection<CellInBattle> fistCollection = GetNeighboursCell((fields[(int)attacking.battlePosition.x, (int)attacking.battlePosition.y]), fields, counAtattackRange); ;
        Collection<CellInBattle> secondCollection = new Collection<CellInBattle>();
        counAtattackRange++;
        while (counAtattackRange <= attacking.rangeWeapone)
        {
            secondCollection.Clear();
            var collection = new Collection<CellInBattle>();
            foreach (CellInBattle cell in fistCollection)
            {
                if (cell.CloseCell)
                {
                    if (fields[(int)cell.PositiongCell.x, (int)cell.PositiongCell.y].PersoneStayInCell.personeType == PersoneType.Player)
                    {
                        playerPersone.Add(fields[(int)cell.PositiongCell.x, (int)cell.PositiongCell.y].PersoneStayInCell);
                    }
                }
                collection = GetNeighboursCell(cell, fields, counAtattackRange);
                secondCollection.AddRange(collection);
               // Debug.Log(cell.positiongGrafCellField);

            }
            fistCollection.Clear();
            fistCollection.AddRange(secondCollection);
            counAtattackRange++;
        }
        foreach (CellInBattle cell in fistCollection)
        {
            if (cell.CloseCell)
            {
                if (fields[(int)cell.PositiongCell.x, (int)cell.PositiongCell.y].PersoneStayInCell.personeType == PersoneType.Player)
                {
                    playerPersone.Add(fields[(int)cell.PositiongCell.x, (int)cell.PositiongCell.y].PersoneStayInCell);
                }
            }
        }

        return playerPersone;

    }
    /// <summary>
    /// получение ближайшей €чейки с которой можно атаковать
    /// </summary>
    /// <param name="fields"></param>
    /// <param name="attacking"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static CellInBattle NeighborCellToAttack(CellInBattle[,] fields, PersoneInBattle attacking, PersoneInBattle target)
    {
        CellInBattle neighborCell = null;
        Vector2 vector = target.battlePosition;//позици€ атакующего
        int counAtattackRange = 1;// счетчик дистанции атаки оружи€
        fields[(int)vector.x, (int)vector.y].AttackRange = attacking.rangeWeapone;// присваивание €чейки где стоит атакующей дистанции оружи€ дл€ закрытие €чейки
        //колекци€ €чеек попадающих в дистанцую атаки
        Collection<CellInBattle> fistCollection = GetNeighboursCell((fields[(int)target.battlePosition.x, (int)target.battlePosition.y]), fields, counAtattackRange); ;
        Collection<CellInBattle> secondCollection = new Collection<CellInBattle>();

        counAtattackRange++;
        while (counAtattackRange <= attacking.rangeWeapone)
        {
            secondCollection.Clear();
            var collection = new Collection<CellInBattle>();
            foreach (CellInBattle cell in fistCollection)
            {
                collection = GetNeighboursCell(cell, fields, counAtattackRange);
                secondCollection.AddRange(collection);
              //  Debug.Log(cell.positiongGrafCellField);

            }
            fistCollection.Clear();
            fistCollection.AddRange(secondCollection);
            counAtattackRange++;
        }
        
        float minDistance = 20;
        float distance = 0;
        foreach (CellInBattle cell in fistCollection)
        {
            distance = Math.Abs(cell.PositiongCell.x - attacking.battlePosition.x) + Math.Abs(cell.PositiongCell.y - attacking.battlePosition.y);
            if (distance < minDistance)
            {
                minDistance = distance;
                neighborCell = cell ;
            }
        }
        return neighborCell;
    }

    private static Collection<CellInBattle> GetNeighboursCell(CellInBattle cellFloor, CellInBattle[,] field, int countRangeAttack)
    {
        var result = new Collection<CellInBattle>();
        // —оседними точками €вл€ютс€ соседние по стороне клетки.
        Vector2 position = cellFloor.PositiongCell;
        Vector2[] neighbourPoints = new Vector2[6];
        if (cellFloor.PositiongCell.y % 2 == 0)
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
            if (!(field[(int)point.x, (int)point.y].AttackRange == 0))
                continue;

            field[(int)point.x, (int)point.y].AttackRange = countRangeAttack;
            // ѕровер€ем, что по клетке можно ходить.
            //  if ((field[(int)point.x, (int)point.y] != 0) && (field[(int)point.x, (int)point.y] != 1))
            //     continue;

            if (countRangeAttack == 1)
            {
                field[(int)point.x, (int)point.y].paintCellBattle(Color.yellow);
            }
            else if (countRangeAttack == 2)
            {
                field[(int)point.x, (int)point.y].paintCellBattle(Color.blue);
            }
            else
            {
                field[(int)point.x, (int)point.y].paintCellBattle(Color.cyan);
            }


            if (field[(int)point.x, (int)point.y].PersoneStayInCell != null)
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
