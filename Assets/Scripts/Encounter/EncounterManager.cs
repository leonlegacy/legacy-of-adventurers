using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public AdvManager adventurerManager;
    public EncounterUI Ui;
    public EncounterObject[] EncounterEvents;

    private EncounterObject occuredEvent;
    private int lootedGold = 0;

    private enum State
    {
        Waiting,
        Result,
        ResultLog,
        Retreat,
    }

    private State currentState = State.Waiting;
    private string resultLog = "";

    /*
     * Get random encounter.
     * Display encounter.
     * Wait for player.
     * Read player action.
     * Result feedback.
     * Effect player.
     * Check party members.
     */

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
        Ui.SetResultLog(resultLog);

        currentState = State.Result;
        DisplayResult(effectDesc);
    }

    private string EffectParty(bool isEffectAll, int damage, int pressure)
    {
        List<Adventurer> members = adventurerManager.PartyMembers;
        string memberStatus = "";
        if(isEffectAll)
        {
            memberStatus += "�����W�[" + pressure + " �I���O\n";
            memberStatus += "��������" + damage + " �I�ˮ`\n";
            members.ForEach(member =>
            {
                member.Health -= damage;
            });
        }
        else
        {
            int index = Random.Range(0, members.Count);
            members[index].Health -= damage;
            if(damage > 0)
                memberStatus += members[index].Name + " ����" + damage + "�I�ˮ`\n";
        }

        foreach(var member in members.ToList())
        {
            if (member.Health <= 0)
            {
                memberStatus += member.Name + " �w�}�`\n";
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
            if(adventurerManager.PartyMembers.Count != 0)
            {
                currentState = State.Waiting;
                // Show Result.
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
            }
        }
    }
}
