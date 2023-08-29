using UnityEngine;
using UnityEngine.SceneManagement;

public class LoseOrWinPanel : MonoBehaviour
{
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private GameObject _WinPanel;
    [SerializeField] private TrackerStatusOfBattle _trakerStatusBattle;


    private void Start()
    {
        _losePanel.SetActive(false);
        _WinPanel.SetActive(false);
        _trakerStatusBattle.OnLoseOrWinPanel += LoseOrWin;
    }

    private void LoseOrWin(bool win)
    {
        if(win == true)
        {
            Time.timeScale = 0;
            _WinPanel.SetActive(true);
        }else if(win == false)
        {
            Time.timeScale = 0;
            _losePanel.SetActive(true);
        }
    }
    public void RestartBattle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FinishBattle()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void UploadSave()
    {
        Time.timeScale = 1;
        //TODO
        Debug.Log("UploadSave //TODO");
    }
}
