using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PersonePanelInBattleUI : MonoBehaviour
{
    [SerializeField] private ProgressBar _progressBarHP;
    [SerializeField] private TextMeshProUGUI _textBarHP;
    [SerializeField] private ProgressBar _progressBarActionPoints;
    [SerializeField] private TextMeshProUGUI _textBarActionPoints;
    [SerializeField] private Image _personeImage;

    [SerializeField] private InitiativeManager _initiativeManager;
    [SerializeField] private PersoneInBattle _currentPersone;

    private void Start()
    {
        _initiativeManager.OnNextPersoneActive += VisablePersone;
    }


    private void OnDestroy()
    {
        _initiativeManager.OnNextPersoneActive -= VisablePersone;
        if (_currentPersone != null)
        {
            _currentPersone.ResetEventListner();
        }
    }

    private void VisablePersone(PersoneInBattle persone)
    {
        if(_currentPersone != null)
        {
            _currentPersone.ResetEventListner();
        }        
        _currentPersone = persone;        
        _personeImage.sprite = persone.Icon;
        _progressBarHP.UpdateProgressBar(persone.MaxHealthPoints, persone.HealthPoint);
        _textBarHP.text = $"{persone.HealthPoint} / {persone.MaxHealthPoints}";
        _progressBarActionPoints.UpdateProgressBar(persone.ActionPointsMax, persone.ActionPoints);
        _textBarActionPoints.text = $"{persone.ActionPoints} / {persone.ActionPointsMax}";
        _currentPersone.ChangeActionPoint += ChangeActionPoints;
        _currentPersone.ChangeHealth += ChangeHealth;
    }
    private void ChangeHealth(int maxHealthPoints,int healthPoint)
    {
        _progressBarHP.UpdateProgressBar(maxHealthPoints, maxHealthPoints);
        _textBarHP.text = $"{maxHealthPoints} / {maxHealthPoints}";
    }
    private void ChangeActionPoints(int actionPointsMax, int actionPoints)
    {
        _progressBarActionPoints.UpdateProgressBar(actionPointsMax, actionPoints);
        _textBarActionPoints.text = $"{actionPoints} / {actionPointsMax}";
    }
}
