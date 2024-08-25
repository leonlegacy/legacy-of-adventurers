using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZhengHua
{ 
    public class GameMainCanvas : CanvasSingtonMono<GameMainCanvas>
    {
        public Transform content;
        public GameObject prefab;
        /// <summary>
        /// 持有金錢
        /// </summary>
        public TMP_Text goldText;
        /// <summary>
        /// 公會聲望
        /// </summary>
        public TMP_Text reputationText;
        /// <summary>
        /// 傭兵僱用費用
        /// </summary>
        public TMP_Text hireCostText;
        /// <summary>
        /// 開始按鈕
        /// </summary>
        public Button startButton;
        /// <summary>
        /// 開始按鈕文字
        /// </summary>
        public TMP_Text startButtonText;

        private List<GameObject> advs = new List<GameObject>();

        private int hireCost = 0;
        public int hireCount = 0;

        public override void Awake()
        {
            base.Awake();

            Hide();
        }

        public override void Show()
        {
            base.Show();

            Initialized();

            UpdateInfo();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialized()
        {
            foreach (var adv in AdvManager.instance.Candidates)
            {
                GameObject obj = Instantiate(prefab, content);
                HireItem handle = obj.GetComponent<HireItem>();
                handle.SetData(adv);
                handle.OnSelectedChangeEvent += UpdateSelected;
                advs.Add(obj);
            }

            hireCost = 0;
        }

        public override void Hide()
        {
            base.Hide();
            GameManager.instance.OnFirstEnterGameOnClick?.Invoke();
            foreach (var adv in advs)
            {
                HireItem handle = adv.GetComponent<HireItem>();
                handle.OnSelectedChangeEvent -= UpdateSelected;
                Destroy(adv);
            }
            advs.Clear();
        }

        private void UpdateSelected(Adventurer data)
        {
            int d = data.IsSelected ? 1 : -1;
            hireCount += d;
            hireCost += d * data.Cost;
            UpdateInfo();
        }

        /// <summary>
        /// 更新僱用費用
        /// </summary>
        /// <param name="cost"></param>
        public void UpdateHireCost(int cost = 0)
        {
            hireCost += cost;
            hireCostText.text = hireCost.ToString();
        }

        /// <summary>
        /// 刷新畫面資料
        /// </summary>
        public void UpdateInfo()
        {
            goldText.text = SaveSystem.instance.playerData.gold.ToString();
            reputationText.text = SaveSystem.instance.playerData.reputation.ToString();
            hireCostText.text = hireCost.ToString();
            UpdateHireCost();
            startButton.interactable = GameManager.instance.ParayIsFull;
            startButtonText.text = GameManager.instance.ParayIsFull ? $"出發" : $"僱傭: {hireCount}/{GameManager.instance.PartyCount}";
        }
    }
}