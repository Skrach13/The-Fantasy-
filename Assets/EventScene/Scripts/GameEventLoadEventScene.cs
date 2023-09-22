using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "StartScene", menuName = "EventScene/Event/StartScene")]
public class GameEventLoadEventScene : GameEvent
{
    [SerializeField] private string _nameScene;
    [SerializeField] private ScenarioEventScene _nextScenarioEventScene;

    public override void StartEvent()
    {
        if (_nextScenarioEventScene == null)
        {
            EventSceneManager.Instance.ScenarioEventScene = null;
        }
        else
        {
            EventSceneManager.Instance.ScenarioEventScene = _nextScenarioEventScene;
        }
        SceneManager.LoadScene(_nameScene);
    }
}
