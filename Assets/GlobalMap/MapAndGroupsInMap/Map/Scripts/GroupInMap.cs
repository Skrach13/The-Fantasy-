using UnityEngine;

[RequireComponent (typeof(RayCastFindAnotherGroup))]
public abstract class GroupInMap : MonoBehaviour
{
    protected RayCastFindAnotherGroup _raycast; 
    [SerializeField] private GroupType _groupType;

    [SerializeField] protected GlobalMapGraf _mapGraf;
    [SerializeField] protected MoveInMap _moveInMap;

    internal GroupType GroupType { get => _groupType; set => _groupType = value; }

    protected void Start()
    {
        _raycast = GetComponent<RayCastFindAnotherGroup>();
    }
}
