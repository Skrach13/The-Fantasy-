using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private ScenarioEventScene _newGameScenarioEvent;
    [SerializeField] private string _newGameSceneName;
    public void StartNewGame()
    {
        EventSceneManager.Instance.ScenarioEventScene = _newGameScenarioEvent;
        SceneManager.LoadScene($"{_newGameSceneName}");
    }
}
