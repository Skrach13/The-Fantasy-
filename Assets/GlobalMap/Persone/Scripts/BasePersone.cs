using System;

[Serializable]
public abstract class BasePersone
{
    private int _ID;
    public string Name = "NoneName";
    public Race Race;
    public int MaxHealth;
    public int Health;
    public Stat[] Stats = new Stat[(int)EStats.Charism+1];   
    public string Description;

   
}
