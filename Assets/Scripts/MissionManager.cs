using Unity.VisualScripting;
using UnityEngine;

public class MissionResult
{
    public bool IsSuccess { get; set; }
    public int Reward { get; set; }
    public int Reputation { get; set; }
}

public class Mission
{

    // 任務名稱
    public string Name;

    // 任務描述
    public string Description;

    // 任務難度
    public int Difficulty;

    // 冒險人數需求
    public int AdventurerCount;

    // 遭遇數量
    public int EncounterCount;

    // 任務酬勞
    public int Reward;

    // 任務成功的抽成比例
    public float SuccessRate;

    // 任務成功的聲望值變化
    public int SuccessReputation;

    // 任務失敗的聲望值變化
    public int FailReputation;

    // 當前遭遇次數
    public int CurrentEncounterTime;


}


public class MissionManager
{
    // 當前任務
    public static Mission CurrentMission { get; private set; }

    /**
     * 生成隨機任務
     * @param count 生成任務的數量(預設為 1 )
     */
    public static Mission GenerateMissions()
    {
        Mission mission = new Mission();

        mission.Name = "Mission " + Random.Range(1, 100);
        mission.Description = "";
        mission.Difficulty = Random.Range(1, 10);
        mission.AdventurerCount = Random.Range(1, 5);
        mission.EncounterCount = Random.Range(1, 5);
        mission.Reward = Random.Range(100, 1000);
        mission.SuccessRate = Random.Range(0.1f, 1.0f);
        mission.SuccessReputation = Random.Range(1, 10);
        mission.FailReputation = Random.Range(1, 10);

        CurrentMission = mission;
        return mission;
    }

    /**
    * 計算任務結果
    * @param isSuccess 是否成功
    */
    public static MissionResult CalculateMissionResult(bool isSuccess)
    {
        var miss = CurrentMission;
        var result = new MissionResult();
        // 任務是否成功
        result.IsSuccess = isSuccess;
        // 任務獎勵(酬勞 * 抽成比例)
        result.Reward = isSuccess ? (int)(miss.Reward * miss.SuccessRate) : 0;
        // 任務聲望
        result.Reputation = isSuccess ? miss.SuccessReputation : miss.FailReputation;
        return result;
    }

}
