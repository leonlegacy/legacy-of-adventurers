using System;
using UnityEngine;

namespace ZhengHua
{ 
    public class GameManager : FiniteStateMachine<GameManager, GameState>
    {
        [SerializeField]
        private string MenuSceneName = "MenuScene";
        public Action OnFirstEnterGameOnClick;

        public override void Awake()
        {
            base.Awake();

            Register(GameState.FirstEnterGame, FirstEnterGameOnEnter, FirstEnterGameOnUpdate, FirstEnterGameOnEnd);
            Register(GameState.Start, StartOnEnter, StartOnUpdate, StartOnEnd);
            Register(GameState.ChooseMission, ChooseMissionOnEnter, ChooseMissionOnUpdate, ChooseMissionOnEnd);
            Register(GameState.InMission, InMissionOnEnter, InMissionOnUpdate, InMissionOnEnd);
            Register(GameState.MissionResult, MissionResultOnEnter, MissionResultOnUpdate, MissionResultOnEnd);
        }

        #region FirstEnterGame
        private void FirstEnterGameOnEnter()
        {
            Debug.Log("FirstEnterGame");
            OnFirstEnterGameOnClick += GoToStart;
            FirstEnterGameCanvas.instance.Show();
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
            ChangeState(GameState.ChooseMission);
        }
        #endregion

        #region Start
        private void StartOnEnter()
        {
            Debug.Log("Start");
            this.SaveGame();
            ChangeState(GameState.ChooseMission);
        }

        private void StartOnUpdate()
        {
            ShowPause();
        }

        private void StartOnEnd()
        {

        }
        #endregion

        #region ChooseMission
        private void ChooseMissionOnEnter()
        {
            Debug.Log("ChooseMission");
            /// 隊伍需要的人員數量
            int partyCount = 4;

            ///初始化玩家隊伍
            AdvManager.instance.PartyInitialize(partyCount);

            /// 初始化招募人員
            AdvManager.instance.GenerateCandidates();

            AdventurerCanvas.instance.Show();
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