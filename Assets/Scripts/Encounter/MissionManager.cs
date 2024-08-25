using System;
using UnityEngine;

public class MissionReport
{
    public int LootGold;
    public int MissionReward;
    public int TotalPressure;
    public int TotalLegacy;
    public float shareRate;
    public MissionReport()
    {

    }
    public MissionReport(int loot, int reward, int pressure, int legacy, float share)
    {
        LootGold = loot;
        MissionReward = reward;
        TotalPressure = pressure;
        TotalLegacy = legacy;
        shareRate = share;
    }
}

public class MissionManagerEvent
{
    public static Action<MissionReport> MissionResultEvent;

    public static void MissionResult(MissionReport report) => MissionResultEvent?.Invoke(report);
}

public class MissionManager : MonoBehaviour
{
    public EncounterManager encounterManager;
    public float shareRate = 0.05f;
    public MissionReport Report = new MissionReport();

    public int EncounterCount
    {
        get { return encounterCount; }
        set { encounterCount = value; }
    }

    public int MissionReward
    {
        get { return missionReward; }
        set { missionReward = value; }
    }

    public int LootedGold { get; set; }
    public int LegacyGain { get; set; }

    private int encounterCount = 5;
    private int missionReward = 0;

    private int currentEncounterIndex = 0;
    //1. (total gold + missionreward) * share-rate
    //2. legacies all.
    //3. total gold * share-rate.

    private int totalGain;


    void MissionInitialize()
    {
        LootedGold = 0;
        LegacyGain = 0;
        totalGain = 0;
        currentEncounterIndex = 0;
        Report.shareRate = shareRate;
    }

    public bool MissionContinue()
    {
        currentEncounterIndex++;
        if(currentEncounterIndex>=encounterCount)
        {
            MissionCompleted();
            return false;
        }
        return true;
    }

    public void MissionCompleted()
    {
        Report.MissionReward = missionReward;
        Report.shareRate = shareRate;
        MissionManagerEvent.MissionResult(Report);
        Debug.Log("Mission Completed.");
        Debug.Log(Report);
    }

    public void MissionFailed()
    {
        Report.MissionReward = 0;
        Report.shareRate = 0;
        MissionManagerEvent.MissionResult(Report);
        Debug.Log("Mission Failed.");
        Debug.Log(Report);
    }

    public void MissionAbort()
    {
        Report.MissionReward = 0;
        Report.shareRate = shareRate;
        MissionManagerEvent.MissionResult(Report);
        Debug.Log("Mission Abort.");
        Debug.Log(Report);
    }
}
