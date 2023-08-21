using UnityEngine;
using static EnumInBattle;

public class EnemyInBattle : PersoneInBattle
{   
    void Start()
    {
        Move = GetComponent<MovePersone>();
        PersoneType = PersoneType.Enemy; 
        ResetPointActioneStartTurn();
    } 
        
}
