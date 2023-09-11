using UnityEngine;

[RequireComponent (typeof(RayCastFindAnotherGroup))]
public abstract class GroupInMap : MonoBehaviour
{
    protected RayCastFindAnotherGroup _raycast; 
    [SerializeField] private GroupType _groupType;    
    [SerializeField] private MoveInMap moveInMap;

    internal GroupType GroupType { get => _groupType; set => _groupType = value; }
    public MoveInMap MoveInMap { get => moveInMap; set => moveInMap = value; }

    protected void Start()
    {
        _raycast = GetComponent<RayCastFindAnotherGroup>();
    }
}
