using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionsBattle : MonoBehaviour
{
    public static void Attack(APersoneScripts attacking, APersoneScripts target)
    {
        target.healthPoint -= attacking.damage;
        Debug.Log("target.healthPoint" + target.healthPoint  + "attacking.damage" +  attacking.damage);
    }
    

}
