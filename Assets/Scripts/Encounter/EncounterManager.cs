using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            resultLog += "��o" + effectGold + "G\n";
        }
        
        resultLog += EffectParty(isEffectAll, effectDamage, effectPressure);

        Debug.Log(resultLog);
        if (resultLog == "")
            resultLog = "�S������S�O���Ƶo��";
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
                memberStatus += "�����W�[" + pressure + " �I���O\n";
            if(damage>0)
                memberStatus += "��������" + damage + " �I�ˮ`\n";
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
                memberStatus += members[index].Name + " �W�[" + pressure + " �I���O\n";
            if (damage > 0)
                memberStatus += members[index].Name + " ����" + damage + " �I�ˮ`\n";
        }

        foreach(var member in members.ToList())
        {
            if (member.Health <= 0)
            {
                memberStatus += member.Name + " �w�}�`\n";
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
                Ui.SetResultLog("�p�������}�`�A�A�a�۶����̿�d�U�Ӫ��˳ƩM�]���M���C���M���ȨS�������A���A�o��F�󦳻��Ȫ��򲣡C");
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
