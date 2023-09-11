using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumInBattle;

/// <summary>
/// ����� ���������� � ���� �������� ������� ���������� �� ��� ( �� ��������� ��������� ���� �������� ��������)
/// </summary>
public class MainBattleSystems : SingletonBase<MainBattleSystems>
{
    [SerializeField] private CellInBattle[,] _cells; //������ ���������� ������ ����

    [SerializeField] private List<PersoneInBattle> _massivePersoneInBattle;// ������ ��� ������� ���������� ( ������� �� List<>)   
    public GameObject _testPreFabPlayer;//������ ���������(����� ��������)
    public GameObject _testPreFabEnemy;//������ ����������(����� ��������)
    
    public PersoneInBattle _activePersone;
    public PersoneInBattle _target;
    
    public bool _playerTurn;
    private int _countPersoneIsRound;
    public ActionType _actionTypePersone;

    public Vector2 _newPosition;//������� ������ ��� ��������� ��������� (����� ��������)
                                //  public Vector2 positionPersone;//
    public int _step;//��� � ������������ ���������
    public bool _personeMove;// ������������� �� ��������
    [SerializeField] private GrafMapInBatle _map;

    public List<Vector2> _path;// ���� ����������� ���������� ��������� ����� � ����� 
    public CellInBattle[,] Cells { get => _cells; set => _cells = value; }
    public List<PersoneInBattle> MassivePersoneInBattle { get => _massivePersoneInBattle; set => _massivePersoneInBattle = value; }
    public GrafMapInBatle Map { get => _map; set => _map = value; }

    private new void Awake()
    {
        base.Awake();
        MassivePersoneInBattle = new List<PersoneInBattle>();
    }

    // Start is called before the first frame update
    void Start()
    {        
        Map.OnCellClicked += StartMove;

        CreatePersoneInBattle(PersoneType.Player, 12, new Vector2(2, 8));
        CreatePersoneInBattle(PersoneType.Player, 15, new Vector2(2, 5));
        CreatePersoneInBattle(PersoneType.Player, 7, new Vector2(2, 2));
        CreatePersoneInBattle(PersoneType.Enemy, 11, new Vector2(8, 8));
        CreatePersoneInBattle(PersoneType.Enemy, 13, new Vector2(8, 5));
        CreatePersoneInBattle(PersoneType.Enemy, 8, new Vector2(8, 2));

        SortIniciative();

        _activePersone = MassivePersoneInBattle[0];
    }
    private void OnDestroy()
    {        
        Map.OnCellClicked += StartMove;
    }

    private void StartMove(List<CellInBattle> path)
    {
       StartCoroutine(_activePersone.Move.PersoneMove(path , Cells));
    }
    /// <summary>
    /// ����� ��� ��������� ��������� �� ���� ��� (���������� ���� �� ����������) 
    /// </summary>
    /// <param name="positionSet"> ������� �� ���� ��������� �� ������ � ������� ���� </param>
    /// <param name="fieldmap">������ ���� ���</param>
    /// <param name="persone">�������� �������� ���� ��������� �� ����</param>
    public void SetPositionPersone(Vector2 positionSet, CellInBattle[,] fieldmap, PersoneInBattle persone)
    {
        var cellField = fieldmap[(int)positionSet.x, (int)positionSet.y];//�������� ��������� ��������� �� ������ ����
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

    public void NextPersoneIniciative()
    {
        _countPersoneIsRound++;

        if (_countPersoneIsRound >= MassivePersoneInBattle.Count)
        {
            _countPersoneIsRound = 0;
        }

        _activePersone = MassivePersoneInBattle[_countPersoneIsRound];
        _activePersone.ResetPointActioneStartTurn();
        _actionTypePersone = ActionType.Move;
        Map.ResetStatsCellFields();
        Debug.Log(_activePersone);
        if (_activePersone.PersoneType == PersoneType.Enemy)
        {
            StartCoroutine(BotInBattle.BotAction(_activePersone));
        }

    }

    /// <summary>
    /// ������������ �� ����� � ���������� �������������� ������� ����� � ������������ ��������� ��� ���� ������(����� ������)
    /// </summary>
    public void changeAttack()
    {
        if (!(_actionTypePersone == ActionType.Attack))
        {
            _actionTypePersone = ActionType.Attack;
            AreaAttack.AttackArea(Cells, _activePersone);
        }
        else
        {
            _actionTypePersone = ActionType.Move;
            Map.ResetStatsCellFields();
        }
    }

    /// <summary>
    /// ���������� ������� ���������� ��� ������ � ����� ������� �����������
    /// </summary>
    public void SortIniciative()
    {
        //��� ��� ����� ��� ��� ��������
        MassivePersoneInBattle.Sort(delegate (PersoneInBattle x, PersoneInBattle y)
        {
            return y.Iniciative.CompareTo(x.Iniciative);
        });
    }

    public void CreatePersoneInBattle(PersoneType personeType, int iniciative, Vector2 startPosition)
    {
        GameObject prefab = null;
        if (personeType == PersoneType.Player)
        {
            prefab = Instantiate(_testPreFabPlayer);// �������� ��������� "������"
        }
        if (personeType == PersoneType.Enemy)
        {
            prefab = Instantiate(_testPreFabEnemy);// �������� ��������� ����������
        }
        PersoneInBattle personeScript;
        personeScript = prefab.GetComponent<PersoneInBattle>();// ���� ������� ��� ������        
        personeScript.Iniciative = iniciative;
        MassivePersoneInBattle.Add(personeScript);// ���� ������� ��� ������
        SetPositionPersone(startPosition, Cells, personeScript); //���� ������� ��� ������

    }
    /// <summary>
    /// ������������ �������� � ���
    /// </summary>
    public enum ActionType
    {
        Move,
        Attack,
        PerformsAction//�������� ��������� ����� �� ��������
    }
}
