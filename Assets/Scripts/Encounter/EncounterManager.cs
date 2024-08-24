using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EncounterManager : MonoBehaviour
{
    public EncounterUI Ui;
    public EncounterObject[] EncounterEvents;

    private EncounterObject occuredEvent;

    private enum State
    {
        Waiting,
        Result,
        Retreat,
    }

    private State currentState = State.Waiting;

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
        if (currentState != State.Result)
            return;

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

        if(dice < occuredEvent.SuccessRate)
        {
            // Success.
            EncounterSuccess();
        }
        else
        {
            // Fail.
            EncounterFail();
        }
        currentState = State.Result;
    }

    private void EncounterSuccess()
    {
        // Get success reward.
        Ui.SetDescription(occuredEvent.SuccessDescription);
        Ui.HideButton(Ui.BtnExecute);
        Ui.HideButton(Ui.BtnRetreat);
        Ui.ShowButton(Ui.BtnContinue);
    }

    private void EncounterFail()
    {
        // Get failure reward.
        Ui.SetDescription(occuredEvent.FailDescription);
        Ui.HideButton(Ui.BtnExecute);
        Ui.HideButton(Ui.BtnRetreat);
        Ui.ShowButton(Ui.BtnContinue);
    }

    private void ContinueMission()
    {
        currentState = State.Waiting;
        Ui.ShowButton(Ui.BtnExecute);
        Ui.ShowButton(Ui.BtnRetreat);
        Ui.HideButton(Ui.BtnContinue);
        GetRandomEncounter();
    }
}
