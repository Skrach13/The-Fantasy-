using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : APersoneScripts
{
    // Start is called before the first frame update
    void Start()
    {
        personeType = PersoneType.Enemy;
        maxHealthPoints = 10;
        movementPointsMax = 10;
        damage = 3;
        rangeWeapone = 5;
        healthPoint = maxHealthPoints;
        ResetPointActioneStartTurn();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.Log(mainSystemBattleScript);
        if (mainSystemBattleScript.activePersone.personeType == PersoneType.Player)
        {
           Debug.Log("mainBattleSystemScripts.target" + mainSystemBattleScript.target);
           mainSystemBattleScript.target = this;
        }
        //Debug.Log(mainBattleSystemScripts.target!=null);
    }

    private void OnMouseExit()
    {
       // Debug.Log("mainBattleSystemScripts.target" + mainBattleSystemScripts.target);
        mainSystemBattleScript.target = null;
    }

    private void OnMouseUp()
    {
        if(((int)mainSystemBattleScript.actionTypePersone) == 1 && mainSystemBattleScript.massiveFields[(int)battlePosition.x,(int)battlePosition.y].attackRange != 0)
        {
            ActionsBattle.Attack(mainSystemBattleScript.activePersone,this);
        }
        Debug.Log("Click Enemy");
    }

    public  IEnumerator AIEnemyActione()
    {
        //опрежелить ближайщего персонажа игрока
        PersoneTest target = null;
        float minDistance = 0; 
        float distance = 0; 
        foreach (PersoneTest player in mainSystemBattleScript.massivePersoneInBattle )
        {
            distance = Math.Abs(player.battlePosition.x - this.battlePosition.x) + Math.Abs(player.battlePosition.y - this.battlePosition.y);
            if (minDistance < distance)
            {
                minDistance = distance;
                target = player;
            }


        }
        Debug.Log(target.battlePosition + " " + distance);
           // yield return null;

        // определить растояние необходимое пройти для атаки
        // пройти растояние необходимое пройти для атаки
        // если можно атаковать => атаковать
        // закончить свой ход 
        mainSystemBattleScript.NextPersoneIniciative();
        yield break;
    }
}
