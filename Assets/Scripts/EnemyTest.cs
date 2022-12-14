using UnityEngine;
using static EnumInBattle;

public class EnemyTest : PersoneInBattle
{
    // Start is called before the first frame update
    void Start()
    {
        personeType = PersoneType.Enemy;
        maxHealthPoints = 10;
        actionPointsMax = 10;
        damage = 3;
        rangeWeapone = 3;
        HealthPoint = maxHealthPoints;
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
        if(((int)mainSystemBattleScript.actionTypePersone) == 1 && mainSystemBattleScript.MassiveFields[(int)battlePosition.x,(int)battlePosition.y].attackRange != 0)
        {
            ActionsBattle.Attack(mainSystemBattleScript.activePersone,this);
        }
        Debug.Log("Click Enemy");
    }
        
}
