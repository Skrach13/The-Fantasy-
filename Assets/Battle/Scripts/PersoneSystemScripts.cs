using UnityEngine;

public class PersoneSystemScripts : MonoBehaviour
{
    public GameObject[] massiveBattleSystemPersone;
    public GameObject[] massiveBattlePlayerPersone;
    public GameObject testPreFabPlayer;
    private GameObject testPreFab;

    // Start is called before the first frame update
    void Start()
    {
        testPreFab = Instantiate(testPreFabPlayer);
        massiveBattlePlayerPersone = new GameObject[1];
        massiveBattlePlayerPersone[0] = testPreFab;
       
    }

    // Update is called once per frame
    void Update()
    {
         
    }
}
