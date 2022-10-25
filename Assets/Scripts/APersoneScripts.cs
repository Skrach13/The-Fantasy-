using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Абстрактный класс для всех персонажей в бою ?
/// </summary>
public abstract class APersoneScripts : MonoBehaviour
{
    public string NamePersone;
    public int healthPoint;
    public int maxHealthPoints;
    public int damage;
    public int iniciative;
    public Vector3 battlePosition;
    public string testgit;
    public int movementPoints;
    public MainBattleSystemScripts mainBattleSystemScripts;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
