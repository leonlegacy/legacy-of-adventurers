using System.Collections.Generic;
using UnityEngine;
using ZhengHua;

public class AdvManager : SingtonMono<AdvManager>
{
    /// <summary>
    /// 可招募人員
    /// </summary>
    public Adventurer[] Candidates;
    /// <summary>
    /// 玩家隊伍
    /// </summary>
    public List<Adventurer> PartyMembers;

    public bool[] Selections = new bool[10];

    private int requiredMembers = 0;
    private int selectedMembers = 0;

    /// <summary>
    /// 初始化招募隊伍
    /// </summary>
    /// <param name="_requiredMembers"></param>
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

    /// <summary>
    /// 初始化招募人員
    /// </summary>
    public void GenerateCandidates()
    {
        //Generate 10 candidates for player to select.

        Candidates = new Adventurer[10];
        var nameGenerator = new NameGenerator();

        for (int i = 0; i < 10; i++)
        {
            Candidates[i] = new Adventurer();
            var candidate = Candidates[i];
            candidate.Name = nameGenerator.RandomName();
            candidate.Health = Random.Range(1, 120);
            candidate.Cost = (int)(candidate.Health * 10);
            candidate.Legacy = Random.Range(100, 250);

            //hp: 1 - 20
            //cost: 1:10 hp
            //worth: 100 - 250
        }
    }

    /// <summary>
    /// 選擇招募人員
    /// </summary>
    /// <param name="id"></param>
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

    /// <summary>
    /// 完成隊伍指派
    /// </summary>
    public void AssignParty()
    {
        //This is called when party members are all set.

        if (selectedMembers < requiredMembers)
        {
            // Warning for not enough members.
            return;
        }

        for (int i = 0; i < Selections.Length; i++)
        {
            if (Selections[i])
                PartyMembers.Add(Candidates[i]);
        }
    }


}
