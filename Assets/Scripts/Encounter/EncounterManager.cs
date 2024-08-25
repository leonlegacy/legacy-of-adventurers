using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManagerEvent
{
    public static System.Action ExecuteEvent;
}

public class EncounterManager : MonoBehaviour
{
    public MissionTracker Mission;
    public EncounterUI Ui;
    public EncounterObject[] EncounterEvents;

    private EncounterObject occuredEvent;
    public int lootedGold = 0;

    private enum State
    {
        Waiting,
        Result,
        ResultLog,
        Retreat,
        PartyWipedout
    }

    private State currentState = State.Waiting;
    private string resultLog = "";

    private void Start()
    {
        GetRandomEncounter();
    }

    public void GetRandomEncounter()
    {
        occuredEvent = EncounterEvents[Random.Range(0, EncounterEvents.Length)];
        UpdateEncounterUI();
        currentState = State.Waiting;
    }

    public void OnExecute()
    {
        if (currentState != State.Waiting)
            return;

        CalculateResult();

        EncounterManagerEvent.ExecuteEvent?.Invoke();
    }

    public void OnRetreat()
    {
        if (currentState != State.Waiting)
            return;

        // Party retreat.
        Mission.MissionAbort();
    }

    public void OnContinue()
    {
        
        ContinueMission();
    }

    void UpdateEncounterUI()
    {
        Ui.SetUIContents(occuredEvent.Image, occuredEvent.Title, occuredEvent.Description);
    }

    void CalculateResult()
    {
        // Random number
        int dice = Random.Range(0, 100);

        bool isEffectAll = occuredEvent.EffectAllMembers;
        int effectGold = 0, effectDamage = 0, effectPressure = 0;
        resultLog = "";

        bool success = (dice < occuredEvent.SuccessRate) ? true : false;
        effectGold = (success) ? occuredEvent.Gold : occuredEvent.FailGold;
        effectDamage = (success) ? occuredEvent.Damage : occuredEvent.FailDamage;
        effectPressure = (success) ? occuredEvent.Pressure : occuredEvent.FailPressure;
        string effectDesc = (success) ? occuredEvent.SuccessDescription : occuredEvent.FailDescription;

        if(lootedGold >0)
        {
            lootedGold += effectGold;
            resultLog += "獲得" + effectGold + "G\n";
        }
        
        resultLog += EffectParty(isEffectAll, effectDamage, effectPressure);

        Debug.Log(resultLog);
        if (resultLog == "")
            resultLog = "沒有什麼特別的事發生";
        Ui.SetResultLog(resultLog);
        Mission.LootedGold = lootedGold;
        currentState = State.Result;
        DisplayResult(effectDesc);
    }

    private string EffectParty(bool isEffectAll, int damage, int pressure)
    {
        List<Adventurer> members = AdvManager.instance.PartyMembers;
        string memberStatus = "";
        if(isEffectAll)
        {
            if(pressure>0)
                memberStatus += "全員增加" + pressure + " 點壓力\n";
            if(damage>0)
                memberStatus += "全員受到" + damage + " 點傷害\n";
            members.ForEach(member =>
            {
                member.Pressure += pressure;
                member.Health -= damage;
            });
        }
        else
        {
            int index = Random.Range(0, members.Count);
            members[index].Pressure += pressure;
            members[index].Health -= damage;
            if (pressure > 0)
                memberStatus += members[index].Name + " 增加" + pressure + " 點壓力\n";
            if (damage > 0)
                memberStatus += members[index].Name + " 受到" + damage + " 點傷害\n";
        }

        foreach(var member in members.ToList())
        {
            if (member.Health <= 0)
            {
                memberStatus += member.Name + " 已陣亡\n";
                Mission.LegacyGain += member.Legacy;
                members.Remove(member);
            }
        }
        return memberStatus;
    }

    private void DisplayResult(string desc)
    {
        Ui.SetDescription(desc);
        Ui.HideButton(Ui.BtnExecute);
        Ui.HideButton(Ui.BtnRetreat);
        Ui.ShowButton(Ui.BtnContinue);
    }

    private void ContinueMission()
    {
        if (currentState == State.Result)
        {
            currentState = State.ResultLog;
            Ui.ShowResult();
        }
        else if (currentState == State.ResultLog)
        {
            if(AdvManager.instance.PartyMembers.Count != 0)
            {
                UpdateReport();
                if (!Mission.MissionContinue())
                    return;

                currentState = State.Waiting;
                // Check in Mission
                Ui.ShowButton(Ui.BtnExecute);
                Ui.ShowButton(Ui.BtnRetreat);
                Ui.HideButton(Ui.BtnContinue);
                Ui.ShowEncounter();
                GetRandomEncounter();
            }
            else
            {
                //GameOver.
                Ui.SetResultLog("小隊全員陣亡，你帶著隊員們遺留下來的裝備和財物撤離。雖然任務沒有完成，但你得到了更有價值的遺產。");
                currentState = State.PartyWipedout;
            }
        }
        else if(currentState == State.PartyWipedout)
        {
            //Mission failed.
            Mission.MissionFailed();
        }
    }

    private void UpdateReport()
    {
        Mission.Report.LootGold = lootedGold;
        Mission.Report.TotalLegacy = Mission.LegacyGain;

        int totalPressure = 0;
        foreach(var member in AdvManager.instance.PartyMembers.ToList())
        {
            totalPressure += member.Pressure;
        }
        Mission.Report.TotalPressure = totalPressure;
    }
}
