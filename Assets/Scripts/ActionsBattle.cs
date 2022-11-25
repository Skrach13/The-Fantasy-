using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsBattle : MonoBehaviour
{
    public static void Attack(PersoneInBattle attacking, PersoneInBattle target)
    {

        target.HealthPoint -= attacking.damage;
        attacking.actionPoints -= 2;
        Debug.Log("target.healthPoint" + target.HealthPoint  + "attacking.damage" +  attacking.damage);
    }
    

}
