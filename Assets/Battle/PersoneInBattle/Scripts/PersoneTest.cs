using static EnumInBattle;

public class PersoneTest : PersoneInBattle
{
    private void Start()
    {
        Move = GetComponent<MovePersone>();
        PersoneType = PersoneType.Player;
        _maxHealthPoints = 10;
        actionPointsMax = 10;
        Damage = 3;
        RangeWeapone = 1;
        HealthPoint = _maxHealthPoints;
        ResetPointActioneStartTurn();

    }

}
