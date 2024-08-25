using UnityEngine;

namespace ZhengHua
{
    public class SaveSystem : Sington<SaveSystem>
    {
        private string saveKey = "Save";

        public bool HaveSave => PlayerPrefs.HasKey(saveKey);

        /// <summary>
        /// 玩家資料
        /// </summary>
        public PlayerData playerData;

        public void Save()
        {
            PlayerPrefs.SetString(saveKey, JsonUtility.ToJson(playerData));
        }

        public void Load()
        {
            if (PlayerPrefs.HasKey(saveKey))
            {
                playerData = JsonUtility.FromJson<PlayerData>(PlayerPrefs.GetString(saveKey));
            }
            else
            {
                playerData = GetDefaultData;
            }
        }

        public bool IsFirstEnterGame 
        { 
            get
            {
                return PlayerPrefs.GetInt("FirstEnterGame", 1) == 1;
            }
            set
            {
                PlayerPrefs.SetInt("FirstEnterGame", value ? 1 : 0);
            }
        }

        public PlayerData GetDefaultData
        {
            get
            {
                return new PlayerData
                {
                    gold = 1000,
                    days = 1,
                    reputation = 80
                };
            }
        }
    }

    public struct PlayerData
    {
        /// <summary>
        /// 金錢
        /// </summary>
        public int gold;
        /// <summary>
        /// 遊戲天數
        /// </summary>
        public int days;
        /// <summary>
        /// 公會聲望值
        /// </summary>
        public int reputation;
    }
}