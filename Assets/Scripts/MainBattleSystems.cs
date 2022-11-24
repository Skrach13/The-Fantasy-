using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ����� ���������� � ���� �������� ������� ���������� �� ��� ( �� ��������� ��������� ���� �������� ��������)
/// </summary>
public class MainBattleSystems : MonoBehaviour
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
    public ActionType actionTypePersone;

    public Vector2 newPosition;//������� ������ ��� ��������� ��������� (����� ��������)
                               //  public Vector2 positionPersone;//

    public int step;//��� � ������������ ���������
    public bool personeMove;// ������������� �� ��������
    public List<Vector2> path;// ���� ����������� ���������� ��������� ����� � ����� 

    private void Awake()
    {
        massiveFields = BattlefieldGeneration.generateFields(widhtField, heightField, PrefloorUnit, gameObject);//�������� ���� ���
        massivePersoneInBattle = new List<APersoneScripts>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // massiveBattlePlayerPersone = new GameObject[1];// ���� ������� ��� ������
        // massiveBattleEnemyPersone = new GameObject[1];// ���� ������� ��� ������
     
        CreatePersoneInBattle(APersoneScripts.PersoneType.Player,12,new Vector2(2,8));
        CreatePersoneInBattle(APersoneScripts.PersoneType.Player,15,new Vector2(2,5));
        CreatePersoneInBattle(APersoneScripts.PersoneType.Player,7,new Vector2(2,2));
        CreatePersoneInBattle(APersoneScripts.PersoneType.Enemy,11,new Vector2(8,8));
        CreatePersoneInBattle(APersoneScripts.PersoneType.Enemy,13,new Vector2(8,5));
        CreatePersoneInBattle(APersoneScripts.PersoneType.Enemy,8,new Vector2(8,2));

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
        personeMove = true;
        int step = 0;
            massiveFields[(int)path[step].x, (int)path[step].y].closeCell = false; //�������� ������ ������ ���� ( ��� � ������ ��������� ��������)
            massiveFields[(int)path[step].x, (int)path[step].y].personeStayInCell = null;
        step++;
        while (true)
        {

            if (step < path.Count && pesrone.actionPoints > 0)// �� ��������� �� ���������� ����� ����� ���� (������ ����?�� ���� ����� ��) � ���� �� ���� ������������ � ���������
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
                    pesrone.actionPoints--;// ���������� ����� �������� ���������
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
    public IEnumerator PersoneMove(APersoneScripts pesrone, List<Vector2> path)//������� ��� ��
    {
        
        int step = 0;
            massiveFields[(int)path[step].x, (int)path[step].y].closeCell = false; //�������� ������ ������ ���� ( ��� � ������ ��������� ��������)
            massiveFields[(int)path[step].x, (int)path[step].y].personeStayInCell = null;
        step++;
        while (true)
        {

            if (step < path.Count && pesrone.actionPoints > 0)// �� ��������� �� ���������� ����� ����� ���� (������ ����?�� ���� ����� ��) � ���� �� ���� ������������ � ���������
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
                    pesrone.actionPoints--;// ���������� ����� �������� ���������
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
        activePersone.ResetPointActioneStartTurn();
        actionTypePersone = ActionType.Move;
        ResetStatsCellFields();
        Debug.Log(activePersone);
        if(activePersone.personeType == APersoneScripts.PersoneType.Enemy)
        {
            StartCoroutine(AIBattle.AIaction(activePersone));
        }

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
        if (!(actionTypePersone == ActionType.Attack))
        {
            actionTypePersone = ActionType.Attack;
            AreaAttack.AttackArea(massiveFields, activePersone);
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
        massivePersoneInBattle.Sort(delegate (APersoneScripts x, APersoneScripts y) {
            return y.iniciative.CompareTo(x.iniciative);
        });
    }

    public void CreatePersoneInBattle(APersoneScripts.PersoneType personeType,int iniciative,Vector2 startPosition)
    {
        var prefab = new GameObject();
        if (personeType == APersoneScripts.PersoneType.Player) 
        {
            prefab = Instantiate(testPreFabPlayer);// �������� ��������� "������"
        }
        if(personeType == APersoneScripts.PersoneType.Enemy) 
        {
            prefab = Instantiate(testPreFabEnemy);// �������� ��������� ����������
        }
            APersoneScripts personeScript;
            personeScript = prefab.GetComponent<APersoneScripts>();// ���� ������� ��� ������
            personeScript.mainSystemBattleScript = this;//���� ������� ��� ������ ��� ������������ ��������� �� ����
            personeScript.iniciative = iniciative;
            massivePersoneInBattle.Add(personeScript);// ���� ������� ��� ������
            SetPositionPersone(startPosition, massiveFields, personeScript); //���� ������� ��� ������
       
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
