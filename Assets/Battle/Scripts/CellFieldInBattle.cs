using UnityEngine;

/// <summary>
/// —крипт €чейки пол€(возможно станет абстрактным)
/// </summary>
public class CellFieldInBattle : MonoBehaviour
{
   
    public Vector2 positiongGrafCellField;
    /// <summary>
    ///ссылка на основной скрипт системы бо€
    /// </summary>
    public MainBattleSystems mainSystemBattleScript;
    public bool closeCell;
    public int attackRange;
    public PersoneInBattle personeStayInCell;
   // public int distanceFromAttacker;

    public SpriteRenderer spriteRenderer;

    private void Awake()
    {
       spriteRenderer = GetComponent<SpriteRenderer>();
    }
   // void Start()
   // {
   // }

    // Update is called once per frame
    void Update()
    {

    }
        
    private void OnMouseEnter()
    {
        //проверка неходит ли сейчас персонаж и закрыта ли €чейка  
        if (!mainSystemBattleScript.personeMove && !closeCell && (mainSystemBattleScript.actionTypePersone == 0))
        {
        //пока не нужно но авось пригодитс€
        //GetComponent<SpriteRenderer>().color = Color.red;
        
         //получение пути от персонажа до этой €чейки
            mainSystemBattleScript.path = PathFinder.Path(mainSystemBattleScript.MassiveFields, mainSystemBattleScript.activePersone.battlePosition, positiongGrafCellField);
        //закрашивание €чеек по сгенерированому пути
            PathFinder.paintPath(mainSystemBattleScript.path, mainSystemBattleScript.MassiveFields, Color.green);
        }
    }
    private void OnMouseExit()
    {
        if (!mainSystemBattleScript.personeMove && (mainSystemBattleScript.actionTypePersone == 0))
        {
            //перекрашивание в стандартный цвет €чеек после выхода курсора мыши из €чейки
            PathFinder.paintPath(mainSystemBattleScript.path, mainSystemBattleScript.MassiveFields, Color.white);
        }
    }
    private void OnMouseDown()
    {
        
    }
    private void OnMouseUp()
    {
        // если €чейка не закрыта\зан€та то после отжати€ мыши над €чейкой начанает движение персонажа
        if (!closeCell && (mainSystemBattleScript.actionTypePersone == 0)) 
        {
            mainSystemBattleScript.personeMove = true;
            StartCoroutine(mainSystemBattleScript.PersoneMove(mainSystemBattleScript.activePersone));
        }
                
    }

    public void paintCellBattle(Color color)
    {
        spriteRenderer.color = color;
    }
}
