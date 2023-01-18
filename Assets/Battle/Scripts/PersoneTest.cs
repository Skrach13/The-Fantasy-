using static EnumInBattle;

public class PersoneTest : PersoneInBattle
{
   // public bool playerActive;
  


    // Start is called before the first frame update
    void Start()
    {
        personeType = PersoneType.Player;
        maxHealthPoints = 10;
        actionPointsMax = 10;
        damage = 3;
        rangeWeapone = 1;
        HealthPoint = maxHealthPoints;
        ResetPointActioneStartTurn();
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   
    
}
