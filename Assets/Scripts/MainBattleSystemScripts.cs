using System.Collections;
using System.Collections.Generic;
using System.IO;
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
  
    public List<APersoneScripts> massiveBattleSystemPersone;// ������ ��� ������� ���������� ( ������� �� List<>)
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


        massiveBattleSystemPersone = new List<APersoneScripts>();
        Debug.Log(massiveBattleSystemPersone);
        // massiveBattlePlayerPersone = new GameObject[1];// ���� ������� ��� ������
        // massiveBattleEnemyPersone = new GameObject[1];// ���� ������� ��� ������
        testPlayer = Instantiate(testPreFabPlayer);// �������� ��������� "������"
        testEnemy = Instantiate(testPreFabEnemy);// �������� ��������� ����������


        testPlayerScript = testPlayer.GetComponent<PersoneTest>();// ���� ������� ��� ������
        testPlayerScript.mainSystemBattleScript = this;//���� ������� ��� ������ ��� ������������ ��������� �� ����
        testEnemyScript = testEnemy.GetComponent<EnemyTest>(); //���� ������� ��� ������
        testEnemyScript.mainSystemBattleScript = this;//���� ������� ��� ������ ��� ������������ ��������� �� ����

        massiveBattleSystemPersone.Add(testPlayerScript);// ���� ������� ��� ������
        massiveBattleSystemPersone.Add(testEnemyScript);// ���� ������� ��� ������


        SetPositionPersone(newPosition, massiveFields, testPlayer); //���� ������� ��� ������
        SetPositionPersone(new Vector2(3, 2), massiveFields, testEnemy);//���� ������� ��� ������
        NextPersone();

        Debug.Log("actionType.Move " + actionType.Move);
        Debug.Log("(int)actionType.Move)  " + (int)actionType.Move);
        Debug.Log("(int)actionType.Move)  " + (int)actionType.Attack);


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
    public void SetPositionPersone(Vector2 positionSet, CellFloorScripts[,] fieldmap, GameObject persone)
    {

        var cellField = fieldmap[(int)positionSet.x, (int)positionSet.y];//�������� ��������� ��������� �� ������ ����
                                                                         // var cellFieldScript = cellField.GetComponent<CellFloorScripts>();//��������� ������ �� ������ ������ ����
        if (!cellField.closeCell) // �������� �� ������ �� 
        {
            var position = cellField.transform.position;//��������� ���������, ���������� ������ �� ������( ��� �� ����� ���� �� � �����)
            position.z = positionSet.y;// ��������� Z ��� ��������� ��������� ( ��� �� ������� ���� ����� ����������� ���������) �� ���������!!!
            persone.GetComponent<APersoneScripts>().battlePosition = cellField.positiongGrafCellField;//������������ ��������� ������� ������ � ����� �� ������� �� �����
            persone.transform.position = position;//��������� ��������� ��������� �� ������
            cellField.closeCell = true;//�������� ������ �� ������� ����� ��������
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

            if (step < path.Count && pesrone.GetComponent<APersoneScripts>().movementPoints > 0)// �� ��������� �� ���������� ����� ����� ���� (������ ����?�� ���� ����� ��) � ���� �� ���� ������������ � ���������
            {
                if (step == 0)
                {
                    massiveFields[(int)path[step].x, (int)path[step].y].GetComponent<CellFloorScripts>().closeCell = false; //�������� ������ ������ ���� ( ��� � ������ ��������� ��������)
                }
                Debug.Log($"Step = :{step}");
                if (pesrone.gameObject.transform.position.x != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.x ||
                    pesrone.gameObject.transform.position.y != massiveFields[(int)path[step].x, (int)path[step].y].transform.position.y)// �������� �� ����� �� ������� ���� ( �������������?)
                {
                    pesrone.gameObject.transform.position = Vector2.MoveTowards(pesrone.gameObject.transform.position, massiveFields[(int)path[step].x, (int)path[step].y].gameObject.transform.position, 0.9f * Time.deltaTime);//�������� � ����� ������ �� ������
                    Debug.Log($"persone move");
                    pesrone.GetComponent<APersoneScripts>().battlePosition = massiveFields[(int)path[step].x, (int)path[step].y].GetComponent<CellFloorScripts>().positiongGrafCellField;//������������ ��������� ������� ����� ������ �� ������� �� �����
                    yield return null;
                }
                else
                {
                    pesrone.GetComponent<APersoneScripts>().movementPoints--;// ���������� ����� �������� ���������
                    step++;// ���������� ������ ����
                }
            }
            else
            {
                massiveFields[(int)pesrone.GetComponent<APersoneScripts>().battlePosition.x, (int)pesrone.GetComponent<APersoneScripts>().battlePosition.y].GetComponent<CellFloorScripts>().closeCell = true;//�������� ������ �� ������� ������ ��������
                personeMove = false;// �������� �� ���������
                step = 0;// ����� �������� �����
                Debug.Log($"��������� ����� ������������ :{pesrone.GetComponent<APersoneScripts>().movementPoints}");
                yield break;

            }

        }
    }

    public void NextPersone()
    {
        Debug.Log("massiveBattleSystemPersone.Count)" + massiveBattleSystemPersone.Count);
        countPersoneIsRound++;

        if (countPersoneIsRound >= massiveBattleSystemPersone.Count)
        {
            countPersoneIsRound = 0;
        }

        activePersone = massiveBattleSystemPersone[countPersoneIsRound];
        activePersone.UpdatingPointStartTurn();
        actionTypePersone = actionType.Move;
        Debug.Log(activePersone);

    }

    public void changeAttack()
    {
        actionTypePersone = actionType.Attack;
    }

    public enum actionType
    {
        Move,
        Attack,
        PerformsAction
    }
}
