using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Persone", menuName = "Persone/Persone", order = 1)]
public class PersoneAssets : ScriptableObject
{
    [Serializable]
    public class PerosneAsset
    {
        public string Name = "NoneName";
        public Image Image;
        public Race Race;
        public StatValue[] Stats;       
        [TextArea(order = 8)] public string Description;
    }
    [Serializable]
    public class StatValue
    {
        public EStats Stat;
        public int Value;
    }
    
    [SerializeField] private PerosneAsset[] _persones;

    public PerosneAsset[] Persones { get => _persones; private set => _persones = value; }
    public PerosneAsset GetPerosne(string name)
    {       
        PerosneAsset perosne = _persones.First(x => x.Name == name);
        return perosne;
    }
}
