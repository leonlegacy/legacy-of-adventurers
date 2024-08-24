using System;

[Serializable]
public class Adventurer
{
    public string Name;
    public bool IsSelected;
    public int Health;
    public int Pressure;
    public int Salary;
    public int Worth;
    public UnityEngine.Sprite avatar;

    public Adventurer()
    {
        Name = "";
        IsSelected = false;
        Health = 0;
        Pressure = 0;
        Salary = 0;
        Worth = 0;
    }
}
