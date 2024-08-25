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
        public Image progressBar;
        /// <summary>
        /// 進度條小人
        /// </summary>
        public Transform progressRunImage;

        /// <summary>
        /// 任務背景
        /// </summary>
        public GameObject tunnel;

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

            UpdateInfo();

            //tunnel.SetActive(true);

            EncounterManagerEvent.ExecuteEvent += Excute;

        }

        private void Excute()
        {
            _runMissionCount++;
            UpdateInfo();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Initialized(int totalMissionCount)
        {
            _runMissionCount = 0;
            _totalMissionCount = totalMissionCount;
            progressBar.fillAmount = 0;
            progressRunImage.localPosition = new Vector3(0, progressRunImage.localPosition.y, 0);
        }

        public override void Hide()
        {
            base.Hide();

            //tunnel.SetActive(false);

            EncounterManagerEvent.ExecuteEvent -= Excute;
        }

        /// <summary>
        /// 刷新畫面資料
        /// </summary>
        public void UpdateInfo()
        {
            goldText.text = SaveSystem.instance.playerData.gold.ToString();
            reputationText.text = $"{SaveSystem.instance.playerData.reputation} / 100";
            reputationImage.fillAmount = (float)SaveSystem.instance.playerData.reputation / 100;

            progressBar.fillAmount = (float)_runMissionCount / _totalMissionCount;
            progressRunImage.localPosition = new Vector3(progressBar.rectTransform.sizeDelta.x * progressBar.fillAmount, progressRunImage.localPosition.y, 0);
        }

        public void UpdateMission()
        {
            _runMissionCount += 1;

            progressBar.fillAmount = (float)_runMissionCount / _totalMissionCount;
            progressRunImage.localPosition = new Vector3(progressBar.rectTransform.sizeDelta.x * progressBar.fillAmount, progressRunImage.localPosition.y, 0);
        }
    }
}