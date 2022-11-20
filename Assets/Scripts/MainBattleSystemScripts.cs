using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

/// <summary>
/// ����� ���������� � ���� �������� ������� ���������� �� ��� ( �� ��������� ��������� ���� �������� ��������)
/// </summary>
public class MainBattleSystemScripts : MonoBehaviour
{

    public int widhtField; // ������ ���� � ���������� �����
    public int heightField; // ������ ���� � ��������� �����
    public GameObject PrefloorUnit;// ������ ������ ���� (�������� �������� ����������?)
    public CellFloorScripts[,] massiveFields; //������ ���������� ������ ����

    public List<APersoneScripts> massivePersoneInBattle;// ������ ��� ������� ���������� ( ������� �� List<>)
    public GameObject[] massiveBattlePlayerPersone;//������ ��� ���������� ������ ( ������ ����� ����� ������� �� List<>)
    public GameObject[] massiveBattleEnemyPersone;//������ ��� ���������� ����������� ( ������ ����� ����� ������� �� List<>)
    public GameObject testPreFabPlayer;//������ ���������(����� ��������)
    public GameObject testPlayer;//������ �� ��������� ������
    public PersoneTest testPlayerScript;//������ �� ������ ���������
    public GameObject testPreFabEnemy;//������ ����������(����� ��������)
    public GameObject testEnemy;// ������ �� ����������
    public EnemyTest testEnemyScript;//������ �� ������ ����������
    public APersoneScripts activePersone;
    public APersoneScripts target;
    public bool playerTurn;
    private int countPersoneIsRound;
    public actionType actionTypePersone;

    public Vector2 newPosition;//������� ������ ��� ��������� ��������� (����� ��������)
                               //  public Vector2 positionPersone;//

    public int step;//��� � ������������ ���������
    public bool personeMove;// ������������� �� ��������
    public List<Vector2> path;// ���� ����������� ���������� ��������� ����� � ����� 

    private void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        massiveFields = BattlefieldGeneration.generateFields(widhtField, heightField, PrefloorUnit, gameObject);//�������� ���� ���

        massivePersoneInBattle = new List<APersoneScripts>();
        // massiveBattlePlayerPersone = new GameObject[1];// ���� ������� ��� ������
        // massiveBattleEnemyPersone = new GameObject[1];// ���� ������� ��� ������
        testPlayer = Instantiate(testPreFabPlayer);// �������� ��������� "������"
        testEnemy = Instantiate(testPreFabEnemy);// �������� ��������� ����������


        testPlayerScript = testPlayer.GetComponent<PersoneTest>();// ���� ������� ��� ������
        testPlayerScript.mainSystemBattleScript = this;//���� ������� ��� ������ ��� ������������ ��������� �� ����
        testPlayerScript.iniciative = 8;
        massivePersoneInBattle.Add(testPlayerScript);// ���� ������� ��� ������
        SetPositionPersone(newPosition, massiveFields, testPlayerScript); //���� ������� ��� ������

        testEnemyScript = testEnemy.GetComponent<EnemyTest>(); //���� ������� ��� ������
        testEnemyScript.iniciative = 7;
        testEnemyScript.mainSystemBattleScript = this;//���� ������� ��� ������ ��� ������������ ��������� �� ����
        massivePersoneInBattle.Add(testEnemyScript);// ���� ������� ��� ������
        SetPositionPersone(new Vector2(3, 2), massiveFields, testEnemyScript);//���� ������� ��� ������

        CreatePersoneInBattle(APersoneScripts.PersoneType.Player,15,new Vector2(4,4));


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
    public void SetPositionPersone(Vector2 positionSet, CellFloorScripts[,] fieldmap, APersoneScripts persone)
    {
        var cellField = fieldmap[(int)positionSet.x, (int)positionSet.y];//�������� ��������� ��������� �� ������ ����
                                                                         // var cellFieldScript = cellField.GetComponent<CellFloorScripts>();//��������� ������ �� ������ ������ ����
        if (!cellField.closeCell) // �������� �� ������ �� 
        {
            var position = cellField.transform.position;//��������� ���������, ���������� ������ �� ������( ��� �� ����� ���� �� � �����)
            position.z = positionSet.y;// ��������� Z ��� ��������� ��������� ( ��� �� ������� ���� ����� ����������� ���������) �� ���������!!!
            persone.battlePosition = cellField.positiongGrafCellField;//������������ ��������� ������� ������ � ����� �� ������� �� �����
            persone.transform.position = position;//��������� ��������� ��������� �� ������
            cellField.closeCell = true;//�������� ������ �� ������� ����� ��������
            cellField.personeStayInCell = persone;//�������� ������ �� ������� ����� ��������
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
    public IEnumerator PersoneMove(APersoneScripts pesrone)
    {
        int step = 0;
        while (true)
        {

            if (step < path.Count && pesrone.movementPoints > 0)// �� ��������� �� ���������� ����� ����� ���� (������ ����?�� ���� ����� ��) � ���� �� ���� ������������ � ���������
            {
                if (step == 0)
                {
                    massiveFields[(int)path[step].x, (int)path[step].y].closeCell = false; //�������� ������ ������ ���� ( ��� � ������ ��������� ��������)
                    massiveFields[(int)path[step].x, (int)path[step].y].personeStayInCell = null;
                }
                if (pesrone.gameObject.transform.position.x != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.x ||
                    pesrone.gameObject.transform.position.y != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.y)// �������� �� ����� �� ������� ���� ( �������������?)
                {
                    pesrone.gameObject.transform.position = Vector2.MoveTowards(pesrone.gameObject.transform.position, massiveFields[(int)path[step].x, (int)path[step].y].gameObject.transform.position, 0.9f * Time.deltaTime);//�������� � ����� ������ �� ������
                    pesrone.battlePosition = massiveFields[(int)path[step].x, (int)path[step].y].positiongGrafCellField;//������������ ��������� ������� ����� ������ �� ������� �� �����
                    yield return null;
                }
                else
                {
                    pesrone.movementPoints--;// ���������� ����� �������� ���������
                    step++;// ���������� ������ ����
                }
            }
            else
            {
                massiveFields[(int)pesrone.battlePosition.x, (int)pesrone.battlePosition.y].closeCell = true;//�������� ������ �� ������� ������ ��������
                massiveFields[(int)pesrone.battlePosition.x, (int)pesrone.battlePosition.y].personeStayInCell = pesrone;
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
        activePersone.UpdatingPointStartTurn();
        actionTypePersone = actionType.Move;
        ResetStatsCellFields();
        Debug.Log(activePersone);

    }
    /// <summary>
    /// ����� ����� ������ � ��������� � ������ �����
    /// </summary>
    public void ResetStatsCellFields()
    {
        for (int i = 0; i < massiveFields.GetLength(0); i++)
        {
            for (int y = 0; y < massiveFields.GetLength(1); y++)
            {
                massiveFields[i, y].paintCellBattle(Color.white);
                massiveFields[i, y].attackRange = 0;
            }
        }
    }

    /// <summary>
    /// ������������ �� ����� � ���������� �������������� ������� ����� � ������������ ��������� ��� ���� ������(����� ������)
    /// </summary>
    public void changeAttack()
    {
        if (!(actionTypePersone == actionType.Attack))
        {
            actionTypePersone = actionType.Attack;
            AreaAttack.AttackArea(massiveFields, activePersone);
        }
        else
        {
            actionTypePersone = actionType.Move;
            ResetStatsCellFields();
        }
    }

    /// <summary>
    /// ���������� ������� ����������
    /// </summary>
    public void SortIniciative()
    {
        massivePersoneInBattle.OrderBy(APersoneScript => APersoneScript.iniciative);
    }

    public void CreatePersoneInBattle(APersoneScripts.PersoneType personeType,int iniciative,Vector2 startPosition)
    {
        APersoneScripts personeScript;
        var prefab = new GameObject();
        if (personeType == APersoneScripts.PersoneType.Player) 
        {
            prefab = Instantiate(testPreFabPlayer);// �������� ��������� "������"
        }
        if(personeType == APersoneScripts.PersoneType.Enemy) 
        {
            prefab = Instantiate(testPreFabEnemy);// �������� ��������� ����������
        }
            personeScript = prefab.GetComponent<APersoneScripts>();// ���� ������� ��� ������
            personeScript.mainSystemBattleScript = this;//���� ������� ��� ������ ��� ������������ ��������� �� ����
            testPlayerScript.iniciative = iniciative;
            massivePersoneInBattle.Add(personeScript);// ���� ������� ��� ������
            SetPositionPersone(startPosition, massiveFields, personeScript); //���� ������� ��� ������
       
    }
    /// <summary>
    /// ������������ �������� � ���
    /// </summary>
    public enum actionType
    {
        Move,
        Attack,
        PerformsAction//�������� ��������� ����� �� ��������
    }
}
