using System;

[Serializable]
public class Adventurer
{
    public string Name;
    public bool IsSelected;
    public int Health;
    public int Pressure;
    public int Cost;
    public int Legacy;
    public UnityEngine.Sprite avatar;
    public bool isMale;

    public Adventurer()
    {
        Name = "";
        IsSelected = false;
        Health = 0;
        Pressure = 0;
        Cost = 0;
        Legacy = 0;
        isMale = true;
    }
}
