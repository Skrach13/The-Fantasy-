using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// Абстрактный класс для всех персонажей в бою ?
/// </summary>
public abstract class APersoneScripts : MonoBehaviour
{
    public string NamePersone;
    public int healthPoint;
    public int maxHealthPoints;
    public int damage;
    public int iniciative;
    public Vector3 battlePosition;
    public string testgit;
    public int actionPointsMax;
    public int actionPoints;
    public int rangeWeapone;
    public MainBattleSystems mainSystemBattleScript;
    public PersoneType personeType;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    public IEnumerator AIEnemyActioner()
    {
        yield return null;
    }

    public void ResetPointActioneStartTurn()
    {
        actionPoints = actionPointsMax;
    }
    public void ResertStats() 
    { 

    }
    
    public enum PersoneType
    {
        Enemy,
        Player,
        Static_object
    }
    





}
