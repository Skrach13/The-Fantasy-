using System.Collections.Generic;
using UnityEngine;

public class InitiativePanelUI : MonoBehaviour
{
    [SerializeField] private GameObject _initiativeLine;
    [SerializeField] private InitiativePersoneObjectUI _prefavInitiativePersone;
    [SerializeField] private List<InitiativePersoneObjectUI> _InitiativePersones = new List<InitiativePersoneObjectUI>();

    [SerializeField] private InitiativeManager _initiativeManager;

    private void Start()
    {
        FillinginitiativeLine();
        _initiativeManager.OnNextPersoneIndex += ChangeInitiativePersone;

    }
    private void FillinginitiativeLine()
    {
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
