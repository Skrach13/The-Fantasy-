using System;
using UnityEditor;
using UnityEngine;

public class RayCastFindAnotherGroup : MonoBehaviour
{
    [SerializeField] private float _radius;
    [SerializeField] private GroupType _groupTypeFind;
    [SerializeField]private bool _isOnActivity;

    public event Action<GroupInMap> OnRaycastHit;
    private Collider2D[] hits;

    public bool IsOnActivity { get => _isOnActivity; set => _isOnActivity = value; }

    private void Update()
    {
        if(IsOnActivity)
        {            
            hits = Physics2D.OverlapCircleAll((Vector2)transform.position, _radius);
            for(int i = 0; i < hits.Length; i++)
            {
                var hit = hits[i].GetComponent<GroupInMap>();
                if (hit != null && hit.GroupType == _groupTypeFind) 
                {
                    OnRaycastHit?.Invoke(hit);
                    IsOnActivity = false;
                }
            }
        }
    }

#if UNITY_EDITOR

    private static Color GizmoColor = new Color(0, 1, 0, 0.3f);

    private void OnDrawGizmosSelected()
    {
        Handles.color = GizmoColor;
        Handles.DrawSolidDisc(transform.position, transform.forward, _radius);
    }

#endif

}
