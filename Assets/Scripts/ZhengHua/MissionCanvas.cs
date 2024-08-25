using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZhengHua
{ 
    public class MissionCanvas : CanvasSingtonMono<MissionCanvas>
    {
        /// <summary>
        /// 持有金錢
        /// </summary>
        public TMP_Text goldText;
        /// <summary>
        /// 公會聲望
        /// </summary>
        public TMP_Text reputationText;
        /// <summary>
        /// 公會聲望 Bar
        /// </summary>
        public Image reputationImage;
        /// <summary>
        /// 任務進度條
        /// </summary>
        public Image missionBar;
        /// <summary>
        /// 進度條小人
        /// </summary>
        public Transform runImage;

        private int _runMissionCount = 0;
        private int _totalMissionCount = 0;

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
            _runMissionCount = 0;
            _totalMissionCount = 0;
            missionBar.fillAmount = 0;
            runImage.localPosition = new Vector3(0, runImage.localPosition.y, 0);
        }

        public override void Hide()
        {
            base.Hide();
        }

        /// <summary>
        /// 刷新畫面資料
        /// </summary>
        public void UpdateInfo()
        {
            goldText.text = SaveSystem.instance.playerData.gold.ToString();
            reputationText.text = $"{SaveSystem.instance.playerData.reputation} / 100";
            reputationImage.fillAmount = (float)SaveSystem.instance.playerData.reputation / 100;


        }

        public void UpdateMission()
        {
            _runMissionCount += 1;

            missionBar.fillAmount = (float)_runMissionCount / _totalMissionCount;
            runImage.localPosition = new Vector3(missionBar.rectTransform.sizeDelta.x * missionBar.fillAmount, runImage.localPosition.y, 0);
        }
    }
}