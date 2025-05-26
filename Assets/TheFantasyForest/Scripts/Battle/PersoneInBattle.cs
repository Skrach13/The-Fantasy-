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
    public AnimationsManagerPersoneInBattle AnimationsManager;
    public List<SkillBase> Skills;
    public Animator Animator;
    public SoundManagerPersone SoundManager;
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
    public Sprite Sprite;

    private MovePersone _move;

    public event Action<PersoneInBattle> OnPersoneEnter;
    public event Action<PersoneInBattle> OnPersoneExit;
    public event Action<PersoneInBattle> OnPersoneClicked;
    public event Action<PersoneInBattle> OnPersoneLose;
    public event Action<int, int> ChangeHealth;
    public event Action<int, int> ChangeActionPoint;

    public MovePersone Move { get => _move; set => _move = value; }
    public SpriteRenderer SpriteRenderer { get => _spriteRenderer; set => _spriteRenderer = value; }
    public int HealthPoint
    {
        get => _healthPoint;
        set
        {
            if (_healthPoint != 0)
            {
                AnimationsManager.HitAnimation();
            }
            _healthPoint = value;
            if (_healthPoint <= 0)
            {
                _healthPoint = 0;
                if (AnimationsManager != null)
                {
                    AnimationsManager.DeadAnimation();
                }
                Lose();
            }
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
        //TODO
    }

    public void Lose()
    {
        OnPersoneLose?.Invoke(this);
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
