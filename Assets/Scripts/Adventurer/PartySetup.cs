using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartySetup : MonoBehaviour
{
    public AdvManager party;

    private void Start()
    {
        party.PartyInitialize(4);
        party.GenerateCandidates();
        party.SelectCandidate(0);
        party.SelectCandidate(6);
        party.SelectCandidate(5);
        party.SelectCandidate(8);
        party.AssignParty();
    }
}
