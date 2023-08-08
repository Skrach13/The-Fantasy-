using static EnumInBattle;

public class PlayerPersoneInBattle : PersoneInBattle
{
    private void Start()
    {
        Move = GetComponent<MovePersone>();
        PersoneType = PersoneType.Player;         
        ResetPointActioneStartTurn();
    }

}
