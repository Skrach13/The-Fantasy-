using UnityEngine;

[CreateAssetMenu(fileName = "AddPersoneInGroup", menuName = "EventScene/Event/AddPersoneInGroup")]
public class AddPersoneInGroup : GameEvent
{
    [SerializeField] private int _indexPlayerPersone;

    public override void StartEvent()
    {
        FindFirstObjectByType<PlayerGroupGlobal>().AddPersoneInGroup(_indexPlayerPersone);
    }
}
