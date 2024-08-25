using System;
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
            /// 隊伍需要的人員數量
            PartyCount = 4;

            ///初始化玩家隊伍
            AdvManager.instance.PartyInitialize(PartyCount);

            /// 初始化招募人員
            AdvManager.instance.GenerateCandidates();

            GameMainCanvas.instance.Show();
        }

        private void ChooseMissionOnUpdate()
        {

        }

        private void ChooseMissionOnEnd()
        {

        }
        #endregion

        #region InMission
        private void InMissionOnEnter()
        {
            Debug.Log("InMission");
        }

        private void InMissionOnUpdate()
        {
        }

        private void InMissionOnEnd()
        {

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