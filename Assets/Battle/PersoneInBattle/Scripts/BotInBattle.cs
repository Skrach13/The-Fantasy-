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

        //TEST TODO 
        enemy.Skills.TryGetValue(KeySkills.AttackMelle, out SkillBase skill);
        //
        while (enemy.ActionPoints > 0)
        {
            MainBattleSystems.Instance.Map.ResetStatsCellFields();
            maybeTarget = DefiningArea.FindAvailableTargets(MainBattleSystems.Instance.Cells, enemy, (SkillActive)enemy.Skills.GetValueOrDefault(KeySkills.AttackMelle));
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
                if (enemy.ActionPoints >= (skill as SkillAttacking).CostUse)
                {                   
                    enemy.AnimationsManager.AttackAnimation();
                    enemy.SoundManager.PlaySoundClip(1);
                    enemy.ActionPoints -= (skill as SkillAttacking).CostUse;
                    (skill as SkillAttacking).UseSkill(target);
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
                
                neighbodCell = DefiningArea.NeighborCellToAttack(MainBattleSystems.Instance.Cells, enemy, target, (SkillActive)enemy.Skills.GetValueOrDefault(KeySkills.AttackMelle));
                var _cellInPath = MainBattleSystems.Instance.Map.GetCellsInPath(enemy.BattlePosition, neighbodCell.PositionInGraff);

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




