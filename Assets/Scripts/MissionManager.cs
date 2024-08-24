using UnityEngine;

public class Result
{
    public bool IsSuccess { get; set; }
    public int Reward { get; set; }
    public int Reputation { get; set; }
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

        MissionManager.CurrentMission = mission;
        return mission;
    }

    /**
    * 計算任務結果
    * @param isSuccess 是否成功
    */
    public static Result CalculateMissionResult(bool isSuccess)
    {
        var miss = CurrentMission;
        var result = new Result();
        result.IsSuccess = isSuccess;
        result.Reward = isSuccess ? (int)(miss.Reward * miss.SuccessRate) : 0;
        result.Reputation = isSuccess ? miss.SuccessReputation : miss.FailReputation;
        return result;
    }

}
