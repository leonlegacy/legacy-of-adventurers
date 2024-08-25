﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZhengHua
{ 
    public class GameManager : FiniteStateMachine<GameManager, GameState>
    {
        [SerializeField]
        private string MenuSceneName = "MenuScene";
        /// <summary>
        /// 用於觸發當第一次進入遊戲時的事件
        /// </summary>
        public Action OnFirstEnterGameOnClick;
        /// <summary>
        /// 進入任務所需要的隊伍人數
        /// </summary>
        public int PartyCount = 4;

        /// <summary>
        /// 冒險者預置物
        /// </summary>
        public GameObject adverPrefab;
        public Transform adverContainer;
        public List<AdventurerItem> adverList = new List<AdventurerItem>();

        public override void Awake()
        {
            base.Awake();

            if (SaveSystem.instance.IsFirstEnterGame)
            {
                Register(GameState.FirstEnterGame, FirstEnterGameOnEnter, FirstEnterGameOnUpdate, FirstEnterGameOnEnd);
            }
            Register(GameState.Start, StartOnEnter, StartOnUpdate, StartOnEnd);
            Register(GameState.ChooseMission, ChooseMissionOnEnter, ChooseMissionOnUpdate, ChooseMissionOnEnd);
            Register(GameState.InMission, InMissionOnEnter, InMissionOnUpdate, InMissionOnEnd);
            Register(GameState.MissionResult, MissionResultOnEnter, MissionResultOnUpdate, MissionResultOnEnd);
        }

        #region FirstEnterGame
        private void FirstEnterGameOnEnter()
        {
            OnFirstEnterGameOnClick += GoToStart;
            FirstEnterGameCanvas.instance.Show();
            SaveSystem.instance.IsFirstEnterGame = false;
        }

        private void FirstEnterGameOnUpdate()
        {
        }

        private void FirstEnterGameOnEnd()
        {
            OnFirstEnterGameOnClick -= GoToStart;
        }

        private void GoToStart()
        {
            Debug.Log("GoToStart");
            ChangeState(GameState.Start);
        }
        #endregion

        #region Start
        private void StartOnEnter()
        {
            Debug.Log("Start");
            this.SaveGame();
            ShowStartAnimation();
        }

        private void StartOnUpdate()
        {
            ShowPause();
        }

        private void StartOnEnd()
        {

        }

        private void ShowStartAnimation()
        {
            Invoke("ShowStartAnimationEnd", 0f);
        }

        private void ShowStartAnimationEnd()
        {
            ChangeState(GameState.ChooseMission);
        }
        #endregion

        #region ChooseMission
        private void ChooseMissionOnEnter()
        {
            Debug.Log("ChooseMission");

            MissionManager.GenerateMissions();

            Mission mission = MissionManager.CurrentMission;

            /// 隊伍需要的人員數量
            PartyCount = mission.AdventurerCount;

            GameMainCanvas.instance.InitMission(mission);

            ///初始化玩家隊伍
            AdvManager.instance.PartyInitialize(PartyCount);

            /// 初始化招募人員
            AdvManager.instance.GenerateCandidates();

            GameMainCanvas.instance.Show();

            GameMainCanvas.instance.PartyGoEvent += PartyGo;
        }

        private void ChooseMissionOnUpdate()
        {
            ShowPause();

        }

        private void ChooseMissionOnEnd()
        {
            GameMainCanvas.instance.PartyGoEvent -= PartyGo;

            GameMainCanvas.instance.Hide();
        }

        private void PartyGo()
        {
            AdvManager.instance.AssignParty();

            ChangeState(GameState.InMission);
        }
        #endregion

        #region InMission
        private void InMissionOnEnter()
        {
            Debug.Log("InMission");
            CreateAdvers();
        }

        private void InMissionOnUpdate()
        {
        }

        private void InMissionOnEnd()
        {

        }

        /// <summary>
        /// 依照隊伍數量建構冒險者
        /// </summary>
        private void CreateAdvers()
        {
            float spaceX = 2.3f;
            float spaceY = 2.3f;
            foreach (var adver in AdvManager.instance.PartyMembers)
            {
                int index = AdvManager.instance.PartyMembers.IndexOf(adver);
                int row = index / 2;
                GameObject obj = Instantiate(adverPrefab, adverContainer);
                AdventurerItem item = obj.GetComponent<AdventurerItem>();
                item.Init(adver.Health, UnityEngine.Random.Range(0, 2) == 1);
                obj.transform.localPosition = new Vector3((index % 2)* spaceX, row * spaceY, 0f);
            }
        }
        #endregion

        #region MissionResult
        private void MissionResultOnEnter()
        {
            Debug.Log("MissionResult");
        }

        private void MissionResultOnUpdate()
        {

        }

        private void MissionResultOnEnd()
        {

        }
        #endregion

        public void ShowPause()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseCanvas.instance.Toggle();
            }
        }

        /// <summary>
        /// 儲存遊戲
        /// </summary>
        public void SaveGame()
        {
            SaveSystem.instance.Save();
        }

        public void BackToMenu()
        {
            LoadManager.instance.LoadScene(MenuSceneName);
        }

        public bool ParayIsFull
        {
            get
            {
                return GameMainCanvas.instance.hireCount == PartyCount;
            }
        }
    }

    public enum GameState
    {
        /// <summary>
        /// 第一次進入遊戲
        /// </summary>
        FirstEnterGame,
        /// <summary>
        /// 遊戲開始
        /// </summary>
        Start,
        /// <summary>
        /// 選擇任務、角色
        /// </summary>
        ChooseMission,
        /// <summary>
        /// 進入任務
        /// </summary>
        InMission,
        /// <summary>
        /// 任務結算
        /// </summary>
        MissionResult,
    }
}