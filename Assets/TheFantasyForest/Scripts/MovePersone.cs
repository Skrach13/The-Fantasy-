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
    /// метод перемещения персонажа по пути из своей позиции на ячейку на поле 
    /// путь всегда храниться в MainBattleSystemScripts
    /// </summary>
    /// <param name="pesrone">Какого персонажа надо переместить</param>
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

            if (step < path.Count && _persone.ActionPoints > 0)// не превышает ли количество шагов длину пути (вообще надо?да надо! зачем хз) и есть ли очки передвижения у персонажа
            {
                    _persone.AnimationsManager.ChangeFlip(Vector3.Normalize( path[step].gameObject.transform.position - transform.position));
                if (transform.position.x != path[step].transform.position.x ||
                    transform.position.y != path[step].transform.position.y)// проверка не вышли за пределы поля ( перестраховка?)
                {
                   transform.position = Vector2.MoveTowards(transform.position, path[step].gameObject.transform.position, _speedMove * Time.deltaTime);//движение с одной ячейки на другую
                    _persone.BattlePosition = path[step].PositionInGraff;//присваевание персанажу позицию графа ячейки на которой он стоит
                    yield return null;
                }
                else
                {
                    _persone.ActionPoints--;// уменьшение очков движения персонажа
                    step++;// увелечения номера шага
                }
            }
            else
            {
                cells[(int)_persone.BattlePosition.x, (int)_persone.BattlePosition.y].CloseCell = true;//закрытие ячейки на которую пришел персонаж
                cells[(int)_persone.BattlePosition.x, (int)_persone.BattlePosition.y].PersoneStayInCell = _persone;
                MainBattleSystems.Instance._personeMove = false;// персонаж не движеться
                step = 0;// сброс счетчика ходов
                MainBattleSystems.Instance.Map.ResetStatsCellFields();
                _persone.AnimationsManager.ChangedWalk(false);
                _persone.SoundManager.StopAudioClip();
                yield break;

            }

        }
    }    
}
