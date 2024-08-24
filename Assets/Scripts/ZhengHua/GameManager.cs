using UnityEngine;

namespace ZhengHua
{ 
    public class GameManager : SingtonMono<GameManager>
    {
        [SerializeField]
        private string MenuSceneName = "MenuScene";

        public GameState gameState = GameState.None;

        public override void Awake()
        {
            base.Awake();
        }
        
        public void Start()
        {
            bool isFirstEnter = PlayerPrefs.GetInt("FirstIn", 0) == 0;
            if (isFirstEnter)
            {
                gameState = GameState.FirstEnterGame;
            }
            else
            {
                gameState = GameState.Start;
            }
        }

        public void Update()
        {
            switch (gameState)
            {
                case GameState.FirstEnterGame:
                    ShowGameTips();
                    break;
                case GameState.Start:
                    ShowPause();
                    break;
                case GameState.ChooseMission:
                    ShowPause();
                    break;
                case GameState.EnterMission:
                    break;
                case GameState.InMission:
                    ShowPause();
                    break;
                case GameState.ChooseMissionEvent:
                    break;
                case GameState.EndMission:
                    break;
                case GameState.MissionResult:
                    break;
                case GameState.Pause:
                    break;
            }
        }

        /// <summary>
        /// 第一次進入遊戲的提示
        /// </summary>
        public void ShowGameTips()
        {
            Debug.Log("ShowGameTips");
            gameState = GameState.Start;
        }

        /// <summary>
        /// 顯示選擇任務的視窗
        /// </summary>
        public void ShowMission()
        {

        }

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
        None,
        FirstEnterGame,
        Start,
        ChooseMission,
        EnterMission,
        InMission,
        ChooseMissionEvent,
        EndMission,
        MissionResult,
        Pause,
    }
}