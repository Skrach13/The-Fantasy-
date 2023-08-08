using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PersonePanel : MonoBehaviour
{
    [SerializeField] private ProgressBar _progressBarHP;
    [SerializeField] private TextMeshProUGUI _textBarHP;
    [SerializeField] private ProgressBar _progressBarActionPoints;
    [SerializeField] private TextMeshProUGUI _textBarActionPoints;
    [SerializeField] private Image _personeImage;

    [SerializeField] private InitiativeManager _initiativeManager;

    private void Start()
    {
        _initiativeManager.OnNextPersoneActive += VisablePersone;
    }


    private void OnDestroy()
    {

    }

    private void VisablePersone(PersoneInBattle persone)
    {
        _personeImage.sprite = persone.Icon;
        _progressBarHP.UpdateProgressBar(persone.MaxHealthPoints, persone.HealthPoint);
        _textBarHP.text = $"{persone.HealthPoint} / {persone.MaxHealthPoints}";
        _progressBarActionPoints.UpdateProgressBar(persone.ActionPointsMax, persone.ActionPoints);
        _textBarActionPoints.text = $"{persone.ActionPoints} / {persone.ActionPointsMax}";
    }
}
