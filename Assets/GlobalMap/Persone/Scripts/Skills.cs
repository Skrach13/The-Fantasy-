using System;
public enum MethodAction
        {
            Active,
            Passive
        }

public partial class Skills
{

    [Serializable]
    public class Atack : SkillBase
    {      
        //public new void Use()
        //{

        //}
    }
    [Serializable]
    public class AtackRange : SkillBase
    {       

    }
}