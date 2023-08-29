using System.Collections.Generic;
using UnityEngine;

public class InitiativePanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _initiativeLine;
    [SerializeField] private InitiativePersoneObjectUI _prefavInitiativePersone;
    [SerializeField] private List<InitiativePersoneObjectUI> _InitiativePersones = new List<InitiativePersoneObjectUI>();

    [SerializeField] private InitiativeManager _initiativeManager;
    [SerializeField] private PersoneGroupsManager _groupManager;

    private void Start()
    {
        FillinginitiativeLine();
        _initiativeManager.OnNextPersoneIndex += ChangeInitiativePersone;
        _groupManager.OnLosePersone += LosePersone;
    }
    private void OnDestroy()
    {
        _groupManager.OnLosePersone -= LosePersone;
    }
    private void LosePersone()
    {
        FillinginitiativeLine();
    }
    private void FillinginitiativeLine()
    {
        if(_initiativeLine.transform.childCount > 0)
        {
            List<Transform> childs = new List<Transform>();
            for(int i = 0;i< _initiativeLine.transform.childCount; i++)
            {
                childs.Add(_initiativeLine.transform.GetChild(i));
            }
            foreach(Transform child in childs)
            {
                Destroy(child.gameObject);
            }
            _InitiativePersones.Clear();
        }
        var persones = MainBattleSystems.Instance.MassivePersoneInBattle;
        for (int i = 0; i < persones.Count; i++)
        {
            _InitiativePersones.Add(Instantiate(_prefavInitiativePersone, _initiativeLine.transform));
            _InitiativePersones[i].IconPersone.sprite = persones[i].Icon;
        }
        _InitiativePersones[0].ImageSelect.enabled = true;
    }

    private void ChangeInitiativePersone(int index)
    {
        if (index - 1 < 0)
        {
            _InitiativePersones[_InitiativePersones.Count - 1].ImageSelect.enabled = false;
        }
        else
        {
            _InitiativePersones[index - 1].ImageSelect.enabled = false;
        }
        _InitiativePersones[index].ImageSelect.enabled = true;
    }

}
