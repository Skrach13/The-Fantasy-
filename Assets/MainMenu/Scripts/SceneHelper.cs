using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHelper : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.F2))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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
    public void Quit()
    {
        Application.Quit();
    }
}
