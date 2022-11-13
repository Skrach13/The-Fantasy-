using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTest : APersoneScripts
{
    // Start is called before the first frame update
    void Start()
    {
        personeType = Persone.Enemy;
        maxHealthPoints = 10;
        movementPointsMax = 10;
        damage = 3;
        rangeWeapone = 2;
        healthPoint = maxHealthPoints;
        UpdatingPointStartTurn();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        Debug.Log(mainSystemBattleScript);
        if (mainSystemBattleScript.activePersone.personeType == Persone.Player)
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

    private void OnMouseDown()
    {
        if(((int)mainSystemBattleScript.actionTypePersone) == 1)
        {
            ActionsBattle.Attack(mainSystemBattleScript.activePersone,this);
        }
    }


}
