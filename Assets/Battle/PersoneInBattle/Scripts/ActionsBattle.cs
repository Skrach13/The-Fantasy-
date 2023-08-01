using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ActionsBattle 
{
    public static void Attack(PersoneInBattle attacking, PersoneInBattle target)
    {

        target.HealthPoint -= attacking.Damage;
        attacking.ActionPoints -= 2;
        Debug.Log("target.healthPoint" + target.HealthPoint  + "attacking.damage" +  attacking.Damage);
    }
    

}
