using System;
using UnityEngine;

[Serializable]
public class SceneFrame
{
    public Sprite[] Sprites;
    [TextArea]public string[] Text;
    
}

[CreateAssetMenu (fileName = "Scenario",menuName = "EventScene/Scenario")]
public class ScenarioEventScene : ScriptableObject
{
    public SceneFrame[] Scenes;
    public GameEvent[] GameEvent;
}
