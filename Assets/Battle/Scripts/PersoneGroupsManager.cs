using System;
using System.Collections.Generic;
using UnityEngine;
using static EnumInBattle;

public class PersoneGroupsManager : MonoBehaviour
{

    [SerializeField] private List<PersoneInBattle> _massivePersoneInBattle = new List<PersoneInBattle>();// ������ ��� ������� ���������� ( ������� �� List<>)   
    [SerializeField] private PlayerPersoneInBattle _preFabPlayer;//������ ���������(����� ��������)
    [SerializeField] private EnemyInBattle _preFabEnemy;//������ ����������(����� ��������)
    [SerializeField] private Vector2[] _positionsPlayer;
    [SerializeField] private Vector2[] _positionsEnemy;

    public event Action<PersoneInBattle> OnPersoneEnterA;
    public event Action<PersoneInBattle> OnPersoneExitA;
    public event Action<PersoneInBattle> OnPersoneClickedA;
    public event Action OnLosePersone;
    public event Action<bool> OnLoseOrWin;
    

    private MainBattleSystems _battleSystems;
    public List<PersoneInBattle> MassivePersoneInBattle { get => _massivePersoneInBattle; set => _massivePersoneInBattle = value; }
    void Start()
    {
        _battleSystems = GetComponent<MainBattleSystems>();

        for (int i = 0; i < GroupGlobalMap.Instance.Group.Count; i++)
        {
            var playerPersone = CreatPlayerPersone(GroupGlobalMap.Instance.Group[i]);
            SetPositionPersone(_positionsPlayer[i], _battleSystems.Cells, playerPersone);
            MassivePersoneInBattle.Add(playerPersone);
        }
        for (int i = 0; i < BattleData.Instance.EnemyProperties.Length; i++)
        {
            var enemy = CreatEnemyPersone(BattleData.Instance.EnemyProperties[i]);
            SetPositionPersone(_positionsEnemy[i], _battleSystems.Cells, enemy);
            MassivePersoneInBattle.Add(enemy);
        }
      foreach(PersoneInBattle perosne in MassivePersoneInBattle)
        {
            perosne.OnPersoneEnter += OnPersoneEnter;
            perosne.OnPersoneExit += OnPersoneExit;
            perosne.OnPersoneClicked += OnPersoneCkliked;
            perosne.OnPersoneLose += LosePerosne;
        }

        SortIniciative();
        MainBattleSystems.Instance.InitiativeManager.NextPersoneIniciative();

    }

    private void OnDestroy()
    {
        foreach (PersoneInBattle perosne in MassivePersoneInBattle)
        {
            perosne.OnPersoneEnter -= OnPersoneEnter;
            perosne.OnPersoneExit -= OnPersoneExit;
            perosne.OnPersoneClicked -= OnPersoneCkliked;
            perosne.OnPersoneLose -= LosePerosne;
        }
    }

    private PlayerPersoneInBattle CreatPlayerPersone(PlayerPersone propertiesPlayer)
    {
        PlayerPersoneInBattle playerPersone = Instantiate(_preFabPlayer);
        playerPersone.PersoneType = PersoneType.Player;
        playerPersone.Skills = propertiesPlayer.Skills;
        playerPersone.NamePersone = propertiesPlayer.Name;
        playerPersone.MaxHealthPoints = propertiesPlayer.Stats[0].Value;

        playerPersone.ActionPointsMax = propertiesPlayer.Stats[2].Value * 5;
        playerPersone.Iniciative = (int)(propertiesPlayer.Stats[2].Value * 1.5f);

        playerPersone.Icon = propertiesPlayer.Icon;
        playerPersone.SpriteRenderer.sprite = playerPersone.Icon;

        playerPersone.HealthPoint = playerPersone.MaxHealthPoints;

        return playerPersone;
    }

    private EnemyInBattle CreatEnemyPersone(EnemyProperties properties)
    {
        EnemyInBattle enemy = Instantiate(_preFabEnemy);
        enemy.NamePersone = properties.name;
        enemy.PersoneType = PersoneType.Enemy;
        enemy.MaxHealthPoints = properties.Stats[0].Value;

        enemy.ActionPointsMax = properties.Stats[2].Value * 5;
        enemy.Iniciative = (int)(properties.Stats[2].Value * 1.5f);

        enemy.Icon = properties.Icon;
        enemy.SpriteRenderer.sprite = properties.Icon;

        enemy.HealthPoint = properties.Stats[0].Value;

        enemy.ActionPoints = enemy.ActionPointsMax;
        //TODO
        enemy.Skills = new Dictionary<KeySkills, SkillBase>();
        enemy.Skills.Add(KeySkills.AttackMelle, properties.SkillAttack);
        //

        return enemy;
    }

    public void SetPositionPersone(Vector2 positionSet, CellInBattle[,] cells, PersoneInBattle persone)
    {
        var cellField = cells[(int)positionSet.x, (int)positionSet.y];//�������� ��������� ��������� �� ������ ����
                                                                      // var cellFieldScript = cellField.GetComponent<CellFloorScripts>();//��������� ������ �� ������ ������ ����
        if (!cellField.CloseCell) // �������� �� ������ �� 
        {
            var position = cellField.transform.position;//��������� ���������, ���������� ������ �� ������( ��� �� ����� ���� �� � �����)
            position.z = positionSet.y;// ��������� Z ��� ��������� ��������� ( ��� �� ������� ���� ����� ����������� ���������) �� ���������!!!
            persone.BattlePosition = cellField.PositionInGraff;//������������ ��������� ������� ������ � ����� �� ������� �� �����
            persone.transform.position = position;//��������� ��������� ��������� �� ������
            cellField.CloseCell = true;//�������� ������ �� ������� ����� ��������
            cellField.PersoneStayInCell = persone;//�������� ������ �� ������� ����� ��������
        }
        else
        {
            Debug.Log("������ ������ ��� �������");
        }
    }
    public void SortIniciative()
    {
        //��� ��� ����� ��� ��� ��������
        MassivePersoneInBattle.Sort(delegate (PersoneInBattle x, PersoneInBattle y)
        {
            return y.Iniciative.CompareTo(x.Iniciative);
        });
    }

    private void LosePerosne(PersoneInBattle persone)
    {
        MassivePersoneInBattle.Remove(persone);
        CellInBattle cell = _battleSystems.Cells[(int)persone.BattlePosition.x, (int)persone.BattlePosition.y];
        cell.PersoneStayInCell = null;
        cell.CloseCell = false;
        Destroy(persone.transform.gameObject);
        SortIniciative();
        OnLosePersone?.Invoke();
        if(MassivePersoneInBattle.Find(playerPersone => playerPersone.PersoneType == PersoneType.Player) == null)
        {
            Debug.Log("Lose");
            OnLoseOrWin?.Invoke(false);
        }
        if (MassivePersoneInBattle.Find(playerPersone => playerPersone.PersoneType == PersoneType.Enemy) == null)
        {
            Debug.Log("Win");
            OnLoseOrWin?.Invoke(true);
        }
    }

    public void OnPersoneEnter(PersoneInBattle persone)
    {
        OnPersoneEnterA?.Invoke(persone);
    }
    public void OnPersoneExit(PersoneInBattle persone)
    {
        OnPersoneExitA?.Invoke(persone);
    }
    public void OnPersoneCkliked(PersoneInBattle persone)
    {
        OnPersoneClickedA?.Invoke(persone);
    }
}
