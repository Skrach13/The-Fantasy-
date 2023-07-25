using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveInMap : MonoBehaviour
{
    [SerializeField] private GlobalMapGraf _globalMapGraf;
    [SerializeField] private Vector2 _positionInMap; 
    [SerializeField] private float _speed;


    private bool _isMove;
    private bool _newStartCoroutin;

    private List<CellBase> _path;
    public List<CellBase> Path { get => _path; set => _path = value; }
    public Vector2 PositionInMap { get => _positionInMap; set => _positionInMap = value; }

    private void Start()
    {        
        transform.position = _globalMapGraf.Cells[(int)PositionInMap.x, (int)PositionInMap.y].transform.position;
    }
    public void StartMove(List<CellBase> cells)
    {
        if (_isMove == false)
        {
            _isMove = true;
            _path = cells;
            StartCoroutine(MovedInPath(_path));
        }
        if (_isMove == true)
        {
            _newStartCoroutin = true;
            _path = cells;
        }

    }
    public IEnumerator MovedInPath(List<CellBase> cells)
    {
        int i = 0;      
        while (i < cells.Count)
        {
            _positionInMap = cells[i].PositionInGraff;
            yield return StartCoroutine(MoveInCell(cells[i].transform.position));
            if (_newStartCoroutin == true)
            {
                _newStartCoroutin = false;
                StartCoroutine(MovedInPath(_path));
                yield break;
            }
            i++;           
        }
        _isMove = false;
        yield break;
    }
    public IEnumerator MoveInCell(Vector2 vector)
    {
        while ((Vector2)transform.position != vector)
        {
            transform.position = Vector2.MoveTowards(transform.position, vector, _speed * Time.deltaTime);
            yield return null;
        }

        yield break;
    }
}
