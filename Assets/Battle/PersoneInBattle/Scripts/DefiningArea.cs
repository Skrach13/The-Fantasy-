using System;
using System.Collections.Generic;
using UnityEngine;
using static EnumInBattle;


public class DefiningArea : MonoBehaviour
{
    /// <summary>
    /// подкрашевание €чеек в радиусе атаки и подкраска €чеек красным цветом на которых стоит персонаж в радиусе атаки
    /// </summary>
    /// <param name="fields"></param>
    /// <param name="attacking"></param>
    public static void DefiningScope(CellInBattle[,] fields, PersoneInBattle attacking,int radius)
    {
        Vector2 vector = attacking.BattlePosition;//позици€ атакующего
        int counAtattackRange = 1;// счетчик дистанции атаки оружи€
        fields[(int)vector.x, (int)vector.y].AttackRange = radius;// присваивание €чейки где стоит атакующей дистанции оружи€ дл€ закрытие €чейки
        //колекци€ €чеек попадающих в дистанцую атаки
        List<CellInBattle> fistCollection = GetNeighboursCell((fields[(int)attacking.BattlePosition.x, (int)attacking.BattlePosition.y]), fields, counAtattackRange); ;
        List<CellInBattle> secondCollection = new List<CellInBattle>();
        counAtattackRange++;
        while (counAtattackRange <= radius)
        {
            secondCollection.Clear();
            var collection = new List<CellInBattle>();
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
    public static List<PersoneInBattle> FindAvailableTargets(CellInBattle[,] fields, PersoneInBattle attacking, SkillActive skill)
    {
        List<PersoneInBattle> playerPersone = new List<PersoneInBattle>();
        Vector2 vector = attacking.BattlePosition;//позици€ атакующего
        int counAtattackRange = 1;// счетчик дистанции атаки оружи€
        fields[(int)vector.x, (int)vector.y].AttackRange = skill.RangeSkill;// присваивание €чейки где стоит атакующей дистанции оружи€ дл€ закрытие €чейки
        //колекци€ €чеек попадающих в дистанцую атаки
        List<CellInBattle> fistCollection = GetNeighboursCell((fields[(int)attacking.BattlePosition.x, (int)attacking.BattlePosition.y]), fields, counAtattackRange); ;
        List<CellInBattle> secondCollection = new List<CellInBattle>();
        counAtattackRange++;
        while (counAtattackRange <= skill.RangeSkill)
        {
            secondCollection.Clear();
            var collection = new List<CellInBattle>();
            foreach (CellInBattle cell in fistCollection)
            {
                if (cell.CloseCell)
                {
                    if (fields[(int)cell.PositionInGraff.x, (int)cell.PositionInGraff.y].PersoneStayInCell.PersoneType == EnumInBattle.PersoneType.Player)
                    {
                        playerPersone.Add(fields[(int)cell.PositionInGraff.x, (int)cell.PositionInGraff.y].PersoneStayInCell);
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
                if (fields[(int)cell.PositionInGraff.x, (int)cell.PositionInGraff.y].PersoneStayInCell.PersoneType == EnumInBattle.PersoneType.Player)
                {
                    playerPersone.Add(fields[(int)cell.PositionInGraff.x, (int)cell.PositionInGraff.y].PersoneStayInCell);
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
    public static CellInBattle NeighborCellToAttack(CellInBattle[,] fields, PersoneInBattle attacking, PersoneInBattle target , SkillActive skill)
    {
        CellInBattle neighborCell = null;
        Vector2 vector = target.BattlePosition;//позици€ атакующего
        int counAtattackRange = 1;// счетчик дистанции атаки оружи€
        fields[(int)vector.x, (int)vector.y].AttackRange = skill.RangeSkill;// присваивание €чейки где стоит атакующей дистанции оружи€ дл€ закрытие €чейки
        //колекци€ €чеек попадающих в дистанцую атаки
        List<CellInBattle> fistCollection = GetNeighboursCell((fields[(int)target.BattlePosition.x, (int)target.BattlePosition.y]), fields, counAtattackRange); ;
        List<CellInBattle> secondCollection = new List<CellInBattle>();

        counAtattackRange++;
        while (counAtattackRange <= skill.RangeSkill)
        {
            secondCollection.Clear();
            var collection = new List<CellInBattle>();
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

        // neighborCell = fistCollection.Min(Cell => Math.Abs(Cell.PositionInGraff.x - attacking.BattlePosition.x) + Math.Abs(Cell.PositionInGraff.y - attacking.BattlePosition.y));
        while (neighborCell == null)
        {
            float minDistance = 20;
            float distance = 0;
            foreach (CellInBattle cell in fistCollection)
            {

                distance = Math.Abs(cell.PositionInGraff.x - attacking.BattlePosition.x) + Math.Abs(cell.PositionInGraff.y - attacking.BattlePosition.y);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    neighborCell = cell;
                }
            }
            if (neighborCell.CloseCell == true)
            {
                fistCollection.Remove(neighborCell);
                neighborCell = null;
            }
        }
        Debug.Log($"{neighborCell}");
        return neighborCell;
    }

    private static List<CellInBattle> GetNeighboursCell(CellInBattle cellFloor, CellInBattle[,] field, int countRangeAttack)
    {
        var result = new List<CellInBattle>();
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
            if (!(field[(int)point.x, (int)point.y].AttackRange == 0))
                continue;

            field[(int)point.x, (int)point.y].AttackRange = countRangeAttack;
            // ѕровер€ем, что по клетке можно ходить.
            //  if ((field[(int)point.x, (int)point.y] != 0) && (field[(int)point.x, (int)point.y] != 1))
            //     continue;

            if (countRangeAttack > 0)
            {
                field[(int)point.x, (int)point.y].PaintCellBattle(ColorsCell.RangeArea);
            }
            
            if (field[(int)point.x, (int)point.y].PersoneStayInCell != null)
            {
                field[(int)point.x, (int)point.y].PaintCellBattle(ColorsCell.Enemies);
            }
            //point.paintCellBattle(Color.red);



            countPoint++;
            result.Add(field[(int)point.x, (int)point.y]);
        }
        return result;
    }



}
