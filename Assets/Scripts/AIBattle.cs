

using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;
using static UnityEngine.GraphicsBuffer;

internal class AIBattle 
{
    public static IEnumerator AIaction(APersoneScripts enemy)
    {
        //определить нет ли персонажа игрока в радиусе атаки
        List<APersoneScripts> maybeTarget = new List<APersoneScripts>();
        APersoneScripts target = null;
        maybeTarget = AreaAttack.PersoneAttackArea(enemy.mainSystemBattleScript.massiveFields,enemy);
        if (maybeTarget.Count > 0)
        {
            foreach (var i in maybeTarget)
            {
                Debug.Log($" возможная цель {i.battlePosition} позиция ИИ {enemy.battlePosition}");
            }
        }

        //опрежелить ближайщего персонажа игрока
        target = neighboringPlayerPersone(enemy);
        List<Vector2> path = PathFinder.Path(enemy.mainSystemBattleScript.massiveFields,enemy.battlePosition,target.battlePosition);
        Debug.Log(path.Count);
        yield return enemy.mainSystemBattleScript.PersoneMove(enemy,path);
        
        // определить растояние необходимое пройти для атаки
        // пройти растояние необходимое пройти для атаки
        // если можно атаковать => атаковать
        // закончить свой ход 
        enemy.mainSystemBattleScript.NextPersoneIniciative();
        yield break;
    }


    static private APersoneScripts neighboringPlayerPersone(APersoneScripts enemy)
    {
        APersoneScripts target = null;
        float minDistance = 20;
        float distance = 0;

        foreach (var player in enemy.mainSystemBattleScript.massivePersoneInBattle)
        {
            if (player.personeType == APersoneScripts.PersoneType.Player)
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




