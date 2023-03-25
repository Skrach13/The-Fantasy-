using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static EnumInBattle;

/// <summary>
/// ����� ���������� � ���� �������� ������� ���������� �� ��� ( �� ��������� ��������� ���� �������� ��������)
/// </summary>
public class MainBattleSystems : MonoBehaviour
{

    public int widhtField; // ������ ���� � ���������� �����
    public int heightField; // ������ ���� � ��������� �����
    public CellInBattle PrefloorUnit;// ������ ������ ���� (�������� �������� ����������?)
    private static CellInBattle[,] massiveFields; //������ ���������� ������ ����

    public List<PersoneInBattle> massivePersoneInBattle;// ������ ��� ������� ���������� ( ������� �� List<>)
    public GameObject[] massiveBattlePlayerPersone;//������ ��� ���������� ������ ( ������ ����� ����� ������� �� List<>)
    public GameObject[] massiveBattleEnemyPersone;//������ ��� ���������� ����������� ( ������ ����� ����� ������� �� List<>)
    public GameObject testPreFabPlayer;//������ ���������(����� ��������)
    public GameObject testPlayer;//������ �� ��������� ������
    public PersoneTest testPlayerScript;//������ �� ������ ���������
    public GameObject testPreFabEnemy;//������ ����������(����� ��������)
    public GameObject testEnemy;// ������ �� ����������
    public EnemyTest testEnemyScript;//������ �� ������ ����������
    public PersoneInBattle activePersone;
    public PersoneInBattle target;
    public bool playerTurn;
    private int countPersoneIsRound;
    public ActionType actionTypePersone;

    public Vector2 newPosition;//������� ������ ��� ��������� ��������� (����� ��������)
                               //  public Vector2 positionPersone;//

    public int step;//��� � ������������ ���������
    public bool personeMove;// ������������� �� ��������
    public List<Vector2> path;// ���� ����������� ���������� ��������� ����� � ����� 

    public CellInBattle[,] MassiveFields { get => massiveFields; set => massiveFields = value; }

    private void Awake()
    {
        MassiveFields = BattleFieldGeneration.GenerateFields(widhtField, heightField, PrefloorUnit, gameObject);//�������� ���� ���
        massivePersoneInBattle = new List<PersoneInBattle>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // massiveBattlePlayerPersone = new GameObject[1];// ���� ������� ��� ������
        // massiveBattleEnemyPersone = new GameObject[1];// ���� ������� ��� ������
     
        CreatePersoneInBattle(PersoneType.Player,12,new Vector2(2,8));
        CreatePersoneInBattle(PersoneType.Player,15,new Vector2(2,5));
        CreatePersoneInBattle(PersoneType.Player,7,new Vector2(2,2));
        CreatePersoneInBattle(PersoneType.Enemy,11,new Vector2(8,8));
        CreatePersoneInBattle(PersoneType.Enemy,13,new Vector2(8,5));
        CreatePersoneInBattle(PersoneType.Enemy,8,new Vector2(8,2));

        SortIniciative();
       
        activePersone = massivePersoneInBattle[0];
       
    }

    // Update is called once per frame
    void Update()
    {

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
            persone.battlePosition = cellField.PositiongCell;//������������ ��������� ������� ������ � ����� �� ������� �� �����
            persone.transform.position = position;//��������� ��������� ��������� �� ������
            cellField.CloseCell = true;//�������� ������ �� ������� ����� ��������
            cellField.PersoneStayInCell = persone;//�������� ������ �� ������� ����� ��������
        }
        else
        {
            Debug.Log("������ ������ ��� �������");
        }
    }
    /// <summary>
    /// ����� ����������� ��������� �� ���� �� ����� ������� �� ������ �� ���� 
    /// ���� ������ ��������� � MainBattleSystemScripts
    /// </summary>
    /// <param name="pesrone">������ ��������� ���� �����������</param>
    public IEnumerator PersoneMove(PersoneInBattle pesrone)
    {
        personeMove = true;
        int step = 0;
            MassiveFields[(int)path[step].x, (int)path[step].y].CloseCell = false; //�������� ������ ������ ���� ( ��� � ������ ��������� ��������)
            MassiveFields[(int)path[step].x, (int)path[step].y].PersoneStayInCell = null;
        step++;
        while (true)
        {

            if (step < path.Count && pesrone.actionPoints > 0)// �� ��������� �� ���������� ����� ����� ���� (������ ����?�� ���� ����� ��) � ���� �� ���� ������������ � ���������
            {
                if (step == 0)
                {
                    MassiveFields[(int)path[step].x, (int)path[step].y].CloseCell = false; //�������� ������ ������ ���� ( ��� � ������ ��������� ��������)
                    MassiveFields[(int)path[step].x, (int)path[step].y].PersoneStayInCell = null;
                }
                if (pesrone.gameObject.transform.position.x != MassiveFields[(int)path[step].x, (int)path[step].y].transform.position.x ||
                    pesrone.gameObject.transform.position.y != MassiveFields[(int)path[step].x, (int)path[step].y].transform.position.y)// �������� �� ����� �� ������� ���� ( �������������?)
                {
                    pesrone.gameObject.transform.position = Vector2.MoveTowards(pesrone.gameObject.transform.position, MassiveFields[(int)path[step].x, (int)path[step].y].gameObject.transform.position, 0.9f * Time.deltaTime);//�������� � ����� ������ �� ������
                    pesrone.battlePosition = MassiveFields[(int)path[step].x, (int)path[step].y].PositiongCell;//������������ ��������� ������� ����� ������ �� ������� �� �����
                    yield return null;
                }
                else
                {
                    pesrone.actionPoints--;// ���������� ����� �������� ���������
                    step++;// ���������� ������ ����
                }
            }
            else
            {
                MassiveFields[(int)pesrone.battlePosition.x, (int)pesrone.battlePosition.y].CloseCell = true;//�������� ������ �� ������� ������ ��������
                MassiveFields[(int)pesrone.battlePosition.x, (int)pesrone.battlePosition.y].PersoneStayInCell = pesrone;
                personeMove = false;// �������� �� ���������
                step = 0;// ����� �������� �����
                ResetStatsCellFields();
                yield break;

            }

        }
    } 
    public IEnumerator PersoneMove(PersoneInBattle pesrone, List<Vector2> path)//������� ��� ��
    {
        
        int step = 0;
            MassiveFields[(int)path[step].x, (int)path[step].y].CloseCell = false; //�������� ������ ������ ���� ( ��� � ������ ��������� ��������)
            MassiveFields[(int)path[step].x, (int)path[step].y].PersoneStayInCell = null;
        step++;
        while (true)
        {

            if (step < path.Count && pesrone.actionPoints > 0)// �� ��������� �� ���������� ����� ����� ���� (������ ����?�� ���� ����� ��) � ���� �� ���� ������������ � ���������
            {
                if (step == 0)
                {
                    MassiveFields[(int)path[step].x, (int)path[step].y].CloseCell = false; //�������� ������ ������ ���� ( ��� � ������ ��������� ��������)
                    MassiveFields[(int)path[step].x, (int)path[step].y].PersoneStayInCell = null;
                }
                if (pesrone.gameObject.transform.position.x != MassiveFields[(int)path[step].x, (int)path[step].y].transform.position.x ||
                    pesrone.gameObject.transform.position.y != MassiveFields[(int)path[step].x, (int)path[step].y].transform.position.y)// �������� �� ����� �� ������� ���� ( �������������?)
                {
                    pesrone.gameObject.transform.position = Vector2.MoveTowards(pesrone.gameObject.transform.position, MassiveFields[(int)path[step].x, (int)path[step].y].gameObject.transform.position, 0.9f * Time.deltaTime);//�������� � ����� ������ �� ������
                    pesrone.battlePosition = MassiveFields[(int)path[step].x, (int)path[step].y].PositiongCell;//������������ ��������� ������� ����� ������ �� ������� �� �����
                    yield return null;
                }
                else
                {
                    pesrone.actionPoints--;// ���������� ����� �������� ���������
                    step++;// ���������� ������ ����
                }
            }
            else
            {
                MassiveFields[(int)pesrone.battlePosition.x, (int)pesrone.battlePosition.y].CloseCell = true;//�������� ������ �� ������� ������ ��������
                MassiveFields[(int)pesrone.battlePosition.x, (int)pesrone.battlePosition.y].PersoneStayInCell = pesrone;
                personeMove = false;// �������� �� ���������
                step = 0;// ����� �������� �����
                ResetStatsCellFields();
                yield break;

            }

        }
    }

    public void NextPersoneIniciative()
    {
        countPersoneIsRound++;

        if (countPersoneIsRound >= massivePersoneInBattle.Count)
        {
            countPersoneIsRound = 0;
        }

        activePersone = massivePersoneInBattle[countPersoneIsRound];
        activePersone.ResetPointActioneStartTurn();
        actionTypePersone = ActionType.Move;
        ResetStatsCellFields();
        Debug.Log(activePersone);
        if(activePersone.personeType == PersoneType.Enemy)
        {
            StartCoroutine(BotInBattle.BotAction(activePersone));
        }

    }
    /// <summary>
    /// ����� ����� ������ � ��������� � ������ �����
    /// </summary>
    public void ResetStatsCellFields()
    {
        for (int i = 0; i < MassiveFields.GetLength(0); i++)
        {
            for (int y = 0; y < MassiveFields.GetLength(1); y++)
            {
                MassiveFields[i, y].paintCellBattle(Color.white);
                MassiveFields[i, y].AttackRange = 0;
            }
        }
    }

    /// <summary>
    /// ������������ �� ����� � ���������� �������������� ������� ����� � ������������ ��������� ��� ���� ������(����� ������)
    /// </summary>
    public void changeAttack()
    {
        if (!(actionTypePersone == ActionType.Attack))
        {
            actionTypePersone = ActionType.Attack;
            AreaAttack.AttackArea(MassiveFields, activePersone);
        }
        else
        {
            actionTypePersone = ActionType.Move;
            ResetStatsCellFields();
        }
    }

    /// <summary>
    /// ���������� ������� ���������� ��� ������ � ����� ������� �����������
    /// </summary>
    public void SortIniciative()
    {
       //��� ��� ����� ��� ��� ��������
        massivePersoneInBattle.Sort(delegate (PersoneInBattle x, PersoneInBattle y) {
            return y.iniciative.CompareTo(x.iniciative);
        });
    }

    public void CreatePersoneInBattle(PersoneType personeType,int iniciative,Vector2 startPosition)
    {
        GameObject prefab = null;
        if (personeType == PersoneType.Player) 
        {
            prefab = Instantiate(testPreFabPlayer);// �������� ��������� "������"
        }
        if(personeType == PersoneType.Enemy) 
        {
            prefab = Instantiate(testPreFabEnemy);// �������� ��������� ����������
        }
            PersoneInBattle personeScript;
            personeScript = prefab.GetComponent<PersoneInBattle>();// ���� ������� ��� ������
            personeScript.mainSystemBattleScript = this;//���� ������� ��� ������ ��� ������������ ��������� �� ����
            personeScript.iniciative = iniciative;
            massivePersoneInBattle.Add(personeScript);// ���� ������� ��� ������
            SetPositionPersone(startPosition, MassiveFields, personeScript); //���� ������� ��� ������
       
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
