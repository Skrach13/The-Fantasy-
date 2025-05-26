using UnityEngine;

public class CreatorAvatarPlayer : MonoBehaviour
{
    [SerializeField] private GameEvent _gameEvent;
    public void CreateAvatarPlayer()
    {
        //TEST
        PlayerGroupGlobal.Instance.AddPersoneInGroup(0);
        _gameEvent.StartEvent();
    }
}
