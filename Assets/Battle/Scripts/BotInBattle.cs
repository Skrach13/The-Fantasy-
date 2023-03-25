using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static EnumInBattle;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

internal class BotInBattle 
{
    public static IEnumerator BotAction(PersoneInBattle enemy)
    {
        //определить нет ли персонажа игрока в радиусе атаки
             List<PersoneInBattle> maybeTarget = null;
             PersoneInBattle target = null;
        while (enemy.actionPoints > 0)
        {
            maybeTarget = AreaAttack.PersoneAttackArea(enemy.mainSystemBattleScript.MassiveFields, enemy);
            if (maybeTarget.Count > 0)
            {
                float minDistance = 20;
                float distance = 0;
                foreach (var playerPersone in maybeTarget)
                {
                    distance = Math.Abs(playerPersone.battlePosition.x - enemy.battlePosition.x) + Math.Abs(playerPersone.battlePosition.y - enemy.battlePosition.y);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        target = playerPersone;
                    }
                    Debug.Log($" возможная цель {playerPersone.battlePosition} позиция ИИ {enemy.battlePosition}");
                }
                if (enemy.actionPoints >= 2)
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

                enemy.mainSystemBattleScript.ResetStatsCellFields();
                CellInBattle neighbodCell = null;
                //опрежелить ближайщего персонажа игрока
                target = neighboringPlayerPersoneFields(enemy);
                neighbodCell = AreaAttack.NeighborCellToAttack(enemy.mainSystemBattleScript.MassiveFields, enemy, target);
                List<Vector2> path = PathFinder.Path(enemy.mainSystemBattleScript.MassiveFields, enemy.battlePosition, neighbodCell.PositiongCell);
                yield return enemy.mainSystemBattleScript.PersoneMove(enemy, path);
            }
            enemy.mainSystemBattleScript.ResetStatsCellFields();
        }
        // определить растояние необходимое пройти для атаки
        // пройти растояние необходимое пройти для атаки
        // если можно атаковать => атаковать
        // закончить свой ход 
        enemy.mainSystemBattleScript.NextPersoneIniciative();
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

        foreach (var player in enemy.mainSystemBattleScript.massivePersoneInBattle)
        {
            if (player.personeType == PersoneType.Player)
            {
                distance = Math.Abs(player.battlePosition.x - enemy.battlePosition.x) + Math.Abs(player.battlePosition.y - enemy.battlePosition.y);
                if (distance < minDistance)
                {
                    minDistance = distance;
                    target = player;
                }
            }
        }

        Debug.Log($" цель {target} позиция цели {target.battlePosition} дистанция до цели {minDistance} позиция ИИ {enemy.battlePosition}");
        return target;
    }
}




