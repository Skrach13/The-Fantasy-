using UnityEngine;
using static EnumInBattle;

public class EnemyInBattle : PersoneInBattle
{   
    void Start()
    {
        Move = GetComponent<MovePersone>();
        PersoneType = PersoneType.Enemy;       
        Damage = 3;
        RangeWeapone = 3;
        
        ResetPointActioneStartTurn();

    }

    private void OnMouseEnter()
    {
        Debug.Log(MainBattleSystems.Instance);
        if (MainBattleSystems.Instance.ActivePersone.PersoneType == PersoneType.Player)
        {
           Debug.Log("mainBattleSystemScripts.target" + MainBattleSystems.Instance.Target);
           MainBattleSystems.Instance.Target = this;
        }       
    }

    private void OnMouseExit()
    {   
        MainBattleSystems.Instance.Target = null;
    }

    private void OnMouseUp()
    {
       
        if(((int)MainBattleSystems.Instance.ActionTypePersone) == 1 && MainBattleSystems.Instance.Cells[(int)BattlePosition.x,(int)BattlePosition.y].AttackRange != 0)
        {
            ActionsBattle.Attack(MainBattleSystems.Instance.ActivePersone,this);
        }
        Debug.Log("Click Enemy");
    }
        
}
