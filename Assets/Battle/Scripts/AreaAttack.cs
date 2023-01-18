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
    public static void AttackArea(CellFieldInBattle[,] fields, PersoneInBattle attacking)
    {
        Vector2 vector = attacking.battlePosition;//позици€ атакующего
        int counAtattackRange = 1;// счетчик дистанции атаки оружи€
        fields[(int)vector.x, (int)vector.y].attackRange = attacking.rangeWeapone;// присваивание €чейки где стоит атакующей дистанции оружи€ дл€ закрытие €чейки
        //колекци€ €чеек попадающих в дистанцую атаки
        Collection<CellFieldInBattle> fistCollection = GetNeighboursCell((fields[(int)attacking.battlePosition.x, (int)attacking.battlePosition.y]), fields, counAtattackRange); ;
        Collection<CellFieldInBattle> secondCollection = new Collection<CellFieldInBattle>();
        counAtattackRange++;
        while (counAtattackRange <= attacking.rangeWeapone)
        {
            secondCollection.Clear();
            var collection = new Collection<CellFieldInBattle>();
            foreach (CellFieldInBattle cell in fistCollection)
            {
                collection = GetNeighboursCell(cell, fields, counAtattackRange);
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
    public static List<PersoneInBattle> PersoneAttackArea(CellFieldInBattle[,] fields, PersoneInBattle attacking)
    {
        List<PersoneInBattle> playerPersone = new List<PersoneInBattle>();
        Vector2 vector = attacking.battlePosition;//позици€ атакующего
        int counAtattackRange = 1;// счетчик дистанции атаки оружи€
        fields[(int)vector.x, (int)vector.y].attackRange = attacking.rangeWeapone;// присваивание €чейки где стоит атакующей дистанции оружи€ дл€ закрытие €чейки
        //колекци€ €чеек попадающих в дистанцую атаки
        Collection<CellFieldInBattle> fistCollection = GetNeighboursCell((fields[(int)attacking.battlePosition.x, (int)attacking.battlePosition.y]), fields, counAtattackRange); ;
        Collection<CellFieldInBattle> secondCollection = new Collection<CellFieldInBattle>();
        counAtattackRange++;
        while (counAtattackRange <= attacking.rangeWeapone)
        {
            secondCollection.Clear();
            var collection = new Collection<CellFieldInBattle>();
            foreach (CellFieldInBattle cell in fistCollection)
            {
                if (cell.closeCell)
                {
                    if (fields[(int)cell.positiongGrafCellField.x, (int)cell.positiongGrafCellField.y].personeStayInCell.personeType == PersoneType.Player)
                    {
                        playerPersone.Add(fields[(int)cell.positiongGrafCellField.x, (int)cell.positiongGrafCellField.y].personeStayInCell);
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
        foreach (CellFieldInBattle cell in fistCollection)
        {
            if (cell.closeCell)
            {
                if (fields[(int)cell.positiongGrafCellField.x, (int)cell.positiongGrafCellField.y].personeStayInCell.personeType == PersoneType.Player)
                {
                    playerPersone.Add(fields[(int)cell.positiongGrafCellField.x, (int)cell.positiongGrafCellField.y].personeStayInCell);
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
    public static CellFieldInBattle NeighborCellToAttack(CellFieldInBattle[,] fields, PersoneInBattle attacking, PersoneInBattle target)
    {
        CellFieldInBattle neighborCell = null;
        Vector2 vector = target.battlePosition;//позици€ атакующего
        int counAtattackRange = 1;// счетчик дистанции атаки оружи€
        fields[(int)vector.x, (int)vector.y].attackRange = attacking.rangeWeapone;// присваивание €чейки где стоит атакующей дистанции оружи€ дл€ закрытие €чейки
        //колекци€ €чеек попадающих в дистанцую атаки
        Collection<CellFieldInBattle> fistCollection = GetNeighboursCell((fields[(int)target.battlePosition.x, (int)target.battlePosition.y]), fields, counAtattackRange); ;
        Collection<CellFieldInBattle> secondCollection = new Collection<CellFieldInBattle>();

        counAtattackRange++;
        while (counAtattackRange <= attacking.rangeWeapone)
        {
            secondCollection.Clear();
            var collection = new Collection<CellFieldInBattle>();
            foreach (CellFieldInBattle cell in fistCollection)
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
        foreach (CellFieldInBattle cell in fistCollection)
        {
            distance = Math.Abs(cell.positiongGrafCellField.x - attacking.battlePosition.x) + Math.Abs(cell.positiongGrafCellField.y - attacking.battlePosition.y);
            if (distance < minDistance)
            {
                minDistance = distance;
                neighborCell = cell ;
            }
        }
        return neighborCell;
    }

    private static Collection<CellFieldInBattle> GetNeighboursCell(CellFieldInBattle cellFloor, CellFieldInBattle[,] field, int countRangeAttack)
    {
        var result = new Collection<CellFieldInBattle>();
        // —оседними точками €вл€ютс€ соседние по стороне клетки.
        Vector2 position = cellFloor.positiongGrafCellField;
        Vector2[] neighbourPoints = new Vector2[6];
        if (cellFloor.positiongGrafCellField.y % 2 == 0)
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
            if (!(field[(int)point.x, (int)point.y].attackRange == 0))
                continue;

            field[(int)point.x, (int)point.y].attackRange = countRangeAttack;
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


            if (field[(int)point.x, (int)point.y].personeStayInCell != null)
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
