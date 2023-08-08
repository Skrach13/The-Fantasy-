using System.Collections.Generic;
using System;
using UnityEngine;
using static EnumInBattle;
using static MainBattleSystems;

public class InitiativeManager : MonoBehaviour
{
    [SerializeField] private PersoneGroupsManager _personeGroupsManager;
    public bool PlayerTurn;
    private int _countPersoneIsRound = -1;
    public ActionType ActionTypePersone;
    public PersoneInBattle ActivePersone;

    public event Action<PersoneInBattle> OnNextPersoneActive;

    private void Start()
    {
        //     _personeGroupsManager = GetComponent<PersoneGroupsManager>();
    }
    public void NextPersoneIniciative()
    {
        _countPersoneIsRound++;
        var massivePersoneInBattle = _personeGroupsManager.MassivePersoneInBattle;
        if (_countPersoneIsRound >= massivePersoneInBattle.Count)
        {
            _countPersoneIsRound = 0;
        }

        ActivePersone = massivePersoneInBattle[_countPersoneIsRound];
        ActivePersone.ResetPointActioneStartTurn();
        ActionTypePersone = ActionType.Move;
        MainBattleSystems.Instance.Map.ResetStatsCellFields();
        Debug.Log(ActivePersone);
        if (ActivePersone.PersoneType == PersoneType.Player)
        {
            OnNextPersoneActive?.Invoke(ActivePersone);
        }
        if (ActivePersone.PersoneType == PersoneType.Enemy)
        {
            StartCoroutine(BotInBattle.BotAction(ActivePersone));
        }

    }


}
