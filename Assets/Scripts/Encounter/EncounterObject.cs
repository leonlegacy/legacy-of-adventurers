using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="New Encounter", menuName = "Encounter Event")]
public class EncounterObject : ScriptableObject
{
    public string Title;
    //public EncounterType Type;

    [Multiline(3)]
    public string Description;
    public Sprite Image;

    public bool EffectAllMembers;
    public float SuccessRate;

    [Multiline(3)]
    public string SuccessDescription;

    [Multiline(3)]
    public string FailDescription;

    [Header("Result")]
    public int Damage;
    public int Pressure;
    public int Gold;
}

public enum EncounterType
{
    Enemy,
    Trap,
    Treasure
}