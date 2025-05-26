using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePersone : MonoBehaviour
{
    [SerializeField] private float _speedMove;
    private PersoneInBattle _persone;
    private List<Vector2> _path;
  

    public List<Vector2> Path { get => _path; set => _path = value; }
    private void Start()
    {
        _persone = GetComponent<PersoneInBattle>();
    }

    /// <summary>
    /// ����� ����������� ��������� �� ���� �� ����� ������� �� ������ �� ���� 
    /// ���� ������ ��������� � MainBattleSystemScripts
    /// </summary>
    /// <param name="pesrone">������ ��������� ���� �����������</param>
    public IEnumerator PersoneMove(List<CellInBattle> path, CellInBattle[,] cells)
    {
        MainBattleSystems.Instance._personeMove = true;
        _persone.AnimationsManager.ChangedWalk(true);
        _persone.SoundManager.PlaySoundClip(0);

        int step = 0;
        path[step].CloseCell = false;
        path[step].PersoneStayInCell = null;
       
        step++;
        while (true)
        {

            if (step < path.Count && _persone.ActionPoints > 0)// �� ��������� �� ���������� ����� ����� ���� (������ ����?�� ����! ����� ��) � ���� �� ���� ������������ � ���������
            {
                    _persone.AnimationsManager.ChangeFlip(Vector3.Normalize( path[step].gameObject.transform.position - transform.position));
                if (transform.position.x != path[step].transform.position.x ||
                    transform.position.y != path[step].transform.position.y)// �������� �� ����� �� ������� ���� ( �������������?)
                {
                   transform.position = Vector2.MoveTowards(transform.position, path[step].gameObject.transform.position, _speedMove * Time.deltaTime);//�������� � ����� ������ �� ������
                    _persone.BattlePosition = path[step].PositionInGraff;//������������ ��������� ������� ����� ������ �� ������� �� �����
                    yield return null;
                }
                else
                {
                    _persone.ActionPoints--;// ���������� ����� �������� ���������
                    step++;// ���������� ������ ����
                }
            }
            else
            {
                cells[(int)_persone.BattlePosition.x, (int)_persone.BattlePosition.y].CloseCell = true;//�������� ������ �� ������� ������ ��������
                cells[(int)_persone.BattlePosition.x, (int)_persone.BattlePosition.y].PersoneStayInCell = _persone;
                MainBattleSystems.Instance._personeMove = false;// �������� �� ���������
                step = 0;// ����� �������� �����
                MainBattleSystems.Instance.Map.ResetStatsCellFields();
                _persone.AnimationsManager.ChangedWalk(false);
                _persone.SoundManager.StopAudioClip();
                yield break;

            }

        }
    }    
}
