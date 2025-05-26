using System;
using UnityEngine;

public class TrackerStatusOfBattle : MonoBehaviour
{
    [SerializeField] private PersoneGroupsManager _personeGroupsManager;
    public event Action<bool> OnLoseOrWinPanel;
    private void Start()
    {
        _personeGroupsManager.OnLoseOrWin += LoseOrWinPanel;
    }
    private void OnDestroy()
    {
        _personeGroupsManager.OnLoseOrWin -= LoseOrWinPanel;        
    }

    public void LoseOrWinPanel(bool win)
    {
        OnLoseOrWinPanel?.Invoke(win);
    }
}
