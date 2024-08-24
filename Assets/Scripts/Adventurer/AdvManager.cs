using System.Collections.Generic;
using UnityEngine;

public class AdvManager : MonoBehaviour
{
    public Adventurer[] Candidates;
    public List<Adventurer> PartyMembers;

    public bool[] Selections = new bool[10];

    private int requiredMembers = 0;
    private int selectedMembers = 0;

    public void PartyInitialize(int _requiredMembers)
    {
        // This is called after a mission is selected.
        // Reset the party list and required member amount.

        for (int i = 0; i < Selections.Length; i++)
            Selections[i] = false;

        requiredMembers = _requiredMembers;
        selectedMembers = 0;
        PartyMembers = new List<Adventurer>();
    }

    public void GenerateCandidates()
    {
        //Generate 10 candidates for player to select.

        Candidates = new Adventurer[10];
        var nameGenerator = new NameGenerator();

        for(int i=0; i<10; i++)
        {
            Candidates[i] = new Adventurer();
            Candidates[i].Name = nameGenerator.RandomName();
        }
    }
    
    public void SelectCandidate(int id)
    {
        //This is called by button for each candidate.

        if (selectedMembers >= requiredMembers)
        {
            // Warning for exceeding amount limit.
            return;
        }    

        Selections[id] = !Selections[id];
        selectedMembers = (Selections[id]) ? selectedMembers + 1 : selectedMembers - 1;
    }

    public void AssignParty()
    {
        //This is called when party members are all set.

        if(selectedMembers < requiredMembers)
        {
            // Warning for not enough members.
            return;
        }

        for(int i=0; i<Selections.Length; i++)
        {
            if (Selections[i])
                PartyMembers.Add(Candidates[i]);
        }
    }

    
}
