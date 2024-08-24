using System.Collections.Generic;
using UnityEngine;

public enum EncounterType
{
    // 戰鬥
    Battle,
    // 陷阱
    Trap,
    // 寶箱
    Chest,
}

public class EncounterOption
{

    // 選項標題
    public string title;

    // 成功率
    public float successRate;

    // 結果成功
    public EncounterResult successResult;

    // 結果失敗
    public EncounterResult failResult;
}

public class EncounterResult
{

    // 結果描述
    public string description;
    // 獲得道具
    public int[] Item;
    // 獲得金額
    public int Gold;
    // 受到的傷害
    public int Damage;

}

public class Encounter
{

    // 標題
    public string title;

    // 圖片ID
    public int spriteID;

    // 描述
    public string description;

    // 選項
    public EncounterOption[] options;

}

public class EncounterManager
{

    public static Encounter currentEncounter { get; set; }

    public static List<Encounter> Encounters = new List<Encounter> {
        new Encounter {
            title = "怪物",
            description = "你遇到了一隻怪物，要不要攻擊呢？",
            spriteID = 0,
            options = new EncounterOption[] {
                new EncounterOption {
                    title = "攻擊",
                    successRate = 0.3f,
                    successResult = new EncounterResult {
                        description = "你成功擊敗了怪物，獲得一百金幣",
                        Item = new int[] { },
                        Gold = 100,
                        Damage = 0,
                    },
                    failResult = new EncounterResult {
                        description = "你被怪物擊敗受了些傷",
                        Item = new int[] {},
                        Gold = 0,
                        Damage = 30,
                    }
                },
                new EncounterOption {
                    title = "逃跑",
                    successRate = 0.7f,
                    successResult = new EncounterResult {
                        description = "你成功逃跑",
                        Item = new int[] { },
                        Gold = 0,
                        Damage = 0,
                    },
                    failResult = new EncounterResult {
                        description = "你被怪物追上痛揍了一頓",
                        Item = new int[] { },
                        Gold = 0,
                        Damage = 20,
                    }
                }
            }
        },
        new Encounter {
            title = "寶箱",
            description = "發現一個寶箱，要不要打開呢？",
            spriteID = 0,
            options = new EncounterOption[] {
                new EncounterOption {
                    title = "打開",
                    successRate = 0.5f,
                    successResult = new EncounterResult {
                        description = "成功開鎖，獲得了寶藏",
                        Item = new int[] { 1 },
                        Gold = 0,
                        Damage = 0,
                    },
                    failResult = new EncounterResult {
                        description = "開鎖失敗，觸發了陷阱",
                        Item = new int[] { },
                        Gold = 0,
                        Damage = 10,
                    }
                },
                new EncounterOption {
                    title = "不打開",
                    successRate = 1f,
                    successResult = new EncounterResult {
                        description = "你選擇直接離開",
                        Item = new int[] { },
                        Gold = 0,
                        Damage = 0,
                    },
                    failResult = new EncounterResult {
                        description = "",
                        Item = new int[] { },
                        Gold = 0,
                        Damage = 0,
                    }
                }
            }
        },
        new Encounter{
            title = "湖中女神",
            description = "在某個房間中間有做小湖，從湖中水面緩緩出現一位女神，女神對你施予治癒術",
            spriteID = 0,
            options = new EncounterOption[] {
                new EncounterOption {
                    title = "受到了治癒",
                    successRate = 1f,
                    successResult = new EncounterResult {
                        description = "你感覺舒服了許多，恢復了一些生命",
                        Item = new int[] {  },
                        Gold = 0,
                        Damage = -30,
                    },
                },
            },
        }
    };

    /**
    * 獲取隨機遭遇
    * @param index 是否指定遭遇索引
    */
    public static Encounter GetRandomEncounter(int index = -1)
    {
        Encounter encounter = index == -1 ? Encounters[Random.Range(0, Encounters.Count)] : Encounters[index];
        currentEncounter = encounter;
        return encounter;
    }

    /**
    * 選項結果
    * @param index 選項索引(理論上是 0 or 1)
    */
    public static EncounterResult isOptionSuccess(int index)
    {
        var option = currentEncounter.options[index];
        return Random.Range(0f, 1f) < option.successRate ? option.successResult : option.failResult;
    }

}