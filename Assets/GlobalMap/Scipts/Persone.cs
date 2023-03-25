using UnityEngine;


[CreateAssetMenu(fileName = "Persone", menuName = "ScriptableObjects/Persone", order = 1)]
public class Persone : ScriptableObject
{
   [SerializeField] public int HitPoints;
    public int MpPoints;
    public int Intelect;
    public int Agility;
    public int Constitution;
}
