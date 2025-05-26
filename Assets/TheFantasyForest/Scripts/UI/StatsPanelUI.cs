using UnityEngine;

public class StatsPanelUI : MonoBehaviour
{
    [SerializeField] private SelectedPersonePanel _panelSelectedPersone;  
    [SerializeField] private StatUI[] _statUI;
   
    private void Start()
    {
        _panelSelectedPersone.OnSelectedPerson += ShowStatsPersone;        
    }

    private void OnEnable()
    {
      
      //  for (int i = 0; i < GroupGlobalMap.Instance.Group.Count; i++)
      //  {           
            ShowStatsPersone(PlayerGroupGlobal.Instance.Group[0].Name);            
     //   }
        
    }    
    
    private void ShowStatsPersone(string name)
    {
        if(_statUI.Length > PlayerGroupGlobal.Instance.GetPerosne(name).Stats.Length) { Debug.LogError("_statUI.Length > _groupGlobalMap.GetPerosne(name).Stats.Length"); }
        for(int i = 0; i < _statUI.Length; i++)
        {          
            _statUI[i].Value.text = PlayerGroupGlobal.Instance.GetPerosne(name).Stats[i+1].Value.ToString();
            _statUI[i].ProgressBar.UpdateProgressBar( PlayerGroupGlobal.Instance.GetPerosne(name).Stats[i + 1].NeededExperience , PlayerGroupGlobal.Instance.GetPerosne(name).Stats[i + 1].UpExperience);
        }       
    }

    private void OnDestroy()
    {
        _panelSelectedPersone.OnSelectedPerson -= ShowStatsPersone;
    }
        
}
