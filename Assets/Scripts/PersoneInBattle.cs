using UnityEngine;
using static EnumInBattle;

/// <summary>
/// Абстрактный класс для всех персонажей в бою ?
/// </summary>
public abstract class PersoneInBattle : MonoBehaviour
{
    public string NamePersone;
    private int healthPoint;
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

    public int HealthPoint { get => healthPoint; set => healthPoint = value; }

    public void ResetPointActioneStartTurn()
    {
        actionPoints = actionPointsMax;
       
    }
    public void ResertStats() 
    { 

    }

    public void Dead()
    {

    }
    
  
    





}
