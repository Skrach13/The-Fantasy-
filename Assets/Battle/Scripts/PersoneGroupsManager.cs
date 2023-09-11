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

        SortIniciative();
        MainBattleSystems.Instance.InitiativeManager.NextPersoneIniciative();
        
    }

    private PlayerPersoneInBattle CreatPlayerPersone(PlayerPersone propertiesPlayer )
    {
        PlayerPersoneInBattle playerPersone = Instantiate(_preFabPlayer);        
        playerPersone.PersoneType = PersoneType.Player;
        playerPersone.NamePersone = propertiesPlayer.Name;
        playerPersone.MaxHealthPoints = propertiesPlayer.Stats[0].Value;

        playerPersone.ActionPointsMax = propertiesPlayer.Stats[2].Value * 5;
        playerPersone.Iniciative = (int)(propertiesPlayer.Stats[2].Value * 1.5f);
        
        playerPersone.Icon = propertiesPlayer.Icon;
        playerPersone.SpriteRenderer.sprite = playerPersone.Icon;
        

        playerPersone.HealthPoint = playerPersone.MaxHealthPoints;

        playerPersone.RangeWeapone = 2;
        playerPersone.Damage = 3;
        
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

        enemy.RangeWeapone = 2;
        enemy.Damage = 3;
       
        enemy.ActionPoints = enemy.ActionPointsMax;
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
}
