using UnityEngine;
using static EnumInBattle;

/// <summary>
/// Абстрактный класс для всех персонажей в бою ?
/// </summary>
public abstract class PersoneInBattle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public string NamePersone;
    public int MaxHealthPoints;
    public int _healthPoint;
    public int ActionPointsMax;
    public int Damage;
    public int Iniciative;
    public Stat[] Stats;
    public PersoneType PersoneType;
    public int ActionPoints;
    public int RangeWeapone;
    public Vector2 BattlePosition;

    public Sprite Icon;

    private MovePersone _move;
    public int HealthPoint {get => _healthPoint; set => _healthPoint = _healthPoint < 0 ? _healthPoint = 0 : _healthPoint = value ;}
  
    public MovePersone Move { get => _move; set => _move = value; }
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }

    public void ResetPointActioneStartTurn()
    {
        ActionPoints = ActionPointsMax;
       
    }
    public void ResetStats() 
    { 

    }

    public void Dead()
    {
        
    }
    
  
    





}
