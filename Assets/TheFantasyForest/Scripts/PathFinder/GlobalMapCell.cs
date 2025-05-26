using System;
using UnityEngine;

[Serializable]
public class GlobalMapCell : CellBase
{
    public event Action<Vector2> OnCellEnter;
    public event Action OnCellExit;
    public event Action OnClickedCell;
    private void OnMouseEnter()
    {

        OnCellEnter?.Invoke(PositionInGraff);
    }

    private void OnMouseDown()
    {
        //  GetLocalPos();
        OnClickedCell?.Invoke();
    }
    private void OnMouseExit()
    {
        OnCellExit?.Invoke();
    }

    public void GetLocalPos()
    {
        Debug.Log(transform.position);
    }

}
