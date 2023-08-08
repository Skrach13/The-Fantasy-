using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumInBattle;

internal class BotInBattle
{
    public static IEnumerator BotAction(PersoneInBattle enemy)
    {
        //определить нет ли персонажа игрока в радиусе атаки
        List<PersoneInBattle> maybeTarget = null;
        PersoneInBattle target = null;
        while (enemy.ActionPoints > 0)
        {
            maybeTarget = AreaAttack.PersoneAttackArea(MainBattleSystems.Instance.Cells, enemy);
            if (maybeTarget.Count > 0)
            {
                float minDistance = 20;
                float distance = 0;
                foreach (var playerPersone in maybeTarget)
                {
                    distance = Math.Abs(playerPersone.BattlePosition.x - enemy.BattlePosition.x) + Math.Abs(playerPersone.BattlePosition.y - enemy.BattlePosition.y);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = playerPersone;
                    }
                    Debug.Log($" возможная цель {playerPersone.BattlePosition} позиция ИИ {enemy.BattlePosition}");
                }
                if (enemy.ActionPoints >= 2)
                {
                    ActionsBattle.Attack(enemy, target);
                }
                else
                {
                    break;
                }
            }
            if (maybeTarget.Count == 0)
            {

                MainBattleSystems.Instance.Map.ResetStatsCellFields();
                CellInBattle neighbodCell = null;
                //опрежелить ближайщего персонажа игрока
                target = neighboringPlayerPersoneFields(enemy);
                neighbodCell = AreaAttack.NeighborCellToAttack(MainBattleSystems.Instance.Cells, enemy, target);
                List<Vector2> path = PathFinderInBattle.Path(MainBattleSystems.Instance.Cells, enemy.BattlePosition, neighbodCell.PositionInGraff);

                var _cellInPath = new List<CellInBattle>();
                for (int i = 0; i < path.Count; i++)
                {
                    _cellInPath.Add(MainBattleSystems.Instance.Cells[(int)path[i].x, (int)path[i].y]);
                }
                yield return enemy.Move.PersoneMove(_cellInPath, MainBattleSystems.Instance.Cells);
            }
            MainBattleSystems.Instance.Map.ResetStatsCellFields();
        }
        // определить растояние необходимое пройти для атаки
        // пройти растояние необходимое пройти для атаки
        // если можно атаковать => атаковать
        // закончить свой ход 
        MainBattleSystems.Instance.InitiativeManager.NextPersoneIniciative();
        yield break;
    }


    /// <summary>
    /// ближайщий игрок по отношению ИИ
    /// </summary>
    /// <param name="enemy"></param>
    /// <returns></returns>
    static private PersoneInBattle neighboringPlayerPersoneFields(PersoneInBattle enemy)
    {
        PersoneInBattle target = null;
        float minDistance = 20;
        float distance = 0;

        foreach (var player in MainBattleSystems.Instance.MassivePersoneInBattle)
        {
            if (player.PersoneType == PersoneType.Player)
            {
                distance = Math.Abs(player.BattlePosition.x - enemy.BattlePosition.x) + Math.Abs(player.BattlePosition.y - enemy.BattlePosition.y);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = player;
                }
            }
        }

        Debug.Log($" цель {target} позиция цели {target.BattlePosition} дистанция до цели {minDistance} позиция ИИ {enemy.BattlePosition}");
        return target;
    }
}




