using UnityEngine;
using static EnumInBattle;

/// <summary>
/// Абстрактный класс для всех персонажей в бою ?
/// </summary>
public abstract class PersoneInBattle : MonoBehaviour
{
    [SerializeField] protected string NamePersone;
    [SerializeField] protected int _healthPoint;
    [SerializeField] protected int _maxHealthPoints;
    [SerializeField] protected int actionPointsMax;
    [SerializeField] protected int _damage;
    [SerializeField] private int iniciative;
    [SerializeField] protected PersoneType personeType;
    public int ActionPoints;
    public int RangeWeapone;
    public Vector2 BattlePosition;

    private MovePersone _move;
    public int HealthPoint {get => _healthPoint; set => _healthPoint = _healthPoint < 0 ? _healthPoint = 0 : _healthPoint = value ;}
    public int Damage { get => _damage; set => _damage = value; }
    public string NamePersone1 { get => NamePersone; set => NamePersone = value; }
    public PersoneType PersoneType { get => personeType; set => personeType = value; }   
    public int Iniciative { get => iniciative; set => iniciative = value; }
    public MovePersone Move { get => _move; set => _move = value; }
    public void ResetPointActioneStartTurn()
    {
        ActionPoints = actionPointsMax;
       
    }
    public void ResetStats() 
    { 

    }

    public void Dead()
    {
        
    }
    
  
    





}
