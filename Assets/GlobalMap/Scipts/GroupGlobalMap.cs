using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class GroupGlobalMap : MonoBehaviour
{
    [SerializeField] private List<Persone> m_Group;

    private void Start()
    {
        m_Group[0].Intelect = 10;
        m_Group[0].HitPoints += 10;
        //CreateInstance(Persone);
        m_Group.Add(ScriptableObject.CreateInstance<Persone>());
        m_Group[2].name = $"tvar{m_Group[2].name}";
        m_Group[2].Agility = 5;
        m_Group[2].Agility += 1;
    //    Debug.Log(m_Group[2].name);
        AssetDatabase.CreateAsset(m_Group[2], $"Assets/1{m_Group[2].Agility}.asset");
        m_Group.Add((Persone)AssetDatabase.LoadAssetAtPath($"Assets/1{m_Group[2].Agility}.asset", typeof(Persone)));
        // m_Group[2].name;
    }
}
