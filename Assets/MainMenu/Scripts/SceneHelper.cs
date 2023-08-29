using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : SingletonBase<SceneHelper>
{
    private void Start()
    {
        //  Cursor.visible = true;
        // Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F2))
        {
            RestartLevel();
        }
        if (Input.GetKeyDown(KeyCode.F1) == true)
        {
            PlayerPrefs.DeleteAll();
        }
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadLevel(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex);
    }
    //TODO разухнать про аддиктивный способ загрузки сцен
    public void LoadLevelAddtivity(int buildIndex)
    {
        SceneManager.LoadScene(buildIndex,LoadSceneMode.Additive);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
