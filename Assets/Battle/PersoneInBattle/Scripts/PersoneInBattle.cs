using System;
using System.Collections.Generic;
using UnityEngine;
using static EnumInBattle;

/// <summary>
/// Абстрактный класс для всех персонажей в бою ?
/// </summary>
public abstract class PersoneInBattle : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    public Dictionary<KeySkills,SkillBase> Skills;
    public string NamePersone;
    public int MaxHealthPoints;
    private int _healthPoint;
    public int ActionPointsMax;
    public int Iniciative;
    public Stat[] Stats;
    public PersoneType PersoneType;
    private int _actionPoints;
    public Vector2 BattlePosition;

    public Sprite Icon;

    private MovePersone _move;

    public event Action<PersoneInBattle> OnPersoneEnter;
    public event Action<PersoneInBattle> OnPersoneExit;
    public event Action<PersoneInBattle> OnPersoneClicked;
    public event Action<int, int> ChangeHealth;
    public event Action<int, int> ChangeActionPoint;

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
    private void OnMouseEnter()
    {       
        if (MainBattleSystems.Instance.ActivePersone.PersoneType == PersoneType.Player)
        {
            MainBattleSystems.Instance.Target = this;
            Debug.Log("mainBattleSystemScripts.target" + MainBattleSystems.Instance.Target);
            OnPersoneEnter?.Invoke(this);
        }
    }

    private void OnMouseExit()
    {
        MainBattleSystems.Instance.Target = null;
        OnPersoneExit?.Invoke(this);
    }

    private void OnMouseDown()
    {  
            OnPersoneClicked?.Invoke(this);
    }
}
