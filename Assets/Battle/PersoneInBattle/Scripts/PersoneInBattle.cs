using System;
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
    private int _healthPoint;
    public int ActionPointsMax;
    public int Damage;
    public int Iniciative;
    public Stat[] Stats;
    public PersoneType PersoneType;
    private int _actionPoints;
    public int RangeWeapone;
    public Vector2 BattlePosition;

    public Sprite Icon;

    private MovePersone _move;

    public MovePersone Move { get => _move; set => _move = value; }
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
    public int HealthPoint
    {
        get => _healthPoint;
        set
        {
            _healthPoint = _healthPoint < 0 ? _healthPoint = 0 : _healthPoint = value;
            ChangeHealth?.Invoke(MaxHealthPoints, _healthPoint);
        }
    }
    public int ActionPoints
    {
        get => _actionPoints;
        set
        {
            _actionPoints = value;

            ChangeActionPoint?.Invoke(ActionPointsMax, _actionPoints);
        }
    }

    public event Action<int, int> ChangeHealth;
    public event Action<int, int> ChangeActionPoint;

    public void ResetPointActioneStartTurn()
    {
        ActionPoints = ActionPointsMax;

    }
    public void ResetEventListner()
    {
        ChangeHealth = null;
        ChangeActionPoint = null;
    }
    public void ResetStats()
    {

    }

    public void Dead()
    {

    }








}
