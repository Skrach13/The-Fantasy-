using UnityEngine;
using static EnumInBattle;

public class EnemyTest : PersoneInBattle
{   
    
    void Start()
    {
        Move = GetComponent<MovePersone>();
        PersoneType = PersoneType.Enemy;
        _maxHealthPoints = 10;
        actionPointsMax = 10;
        Damage = 3;
        RangeWeapone = 3;
        HealthPoint = _maxHealthPoints;
        ResetPointActioneStartTurn();

    }

    private void OnMouseEnter()
    {
        Debug.Log(MainBattleSystems.Instance);
        if (MainBattleSystems.Instance._activePersone.PersoneType == PersoneType.Player)
        {
           Debug.Log("mainBattleSystemScripts.target" + MainBattleSystems.Instance._target);
           MainBattleSystems.Instance._target = this;
        }       
    }

    private void OnMouseExit()
    {   
        MainBattleSystems.Instance._target = null;
    }

    private void OnMouseUp()
    {
       
        if(((int)MainBattleSystems.Instance._actionTypePersone) == 1 && MainBattleSystems.Instance.Cells[(int)BattlePosition.x,(int)BattlePosition.y].AttackRange != 0)
        {
            ActionsBattle.Attack(MainBattleSystems.Instance._activePersone,this);
        }
        Debug.Log("Click Enemy");
    }
        
}
