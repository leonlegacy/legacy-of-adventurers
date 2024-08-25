using UnityEngine;

namespace ZhengHua
{ 
    public class MenuManager : SingtonMono<MenuManager>
    {
        [SerializeField]
        private string GameSceneName = "GameScene";

        private MenuButton newGameButton;
        private MenuButton continueButton;
        private MenuButton exitButton;

        public override void Awake()
        {
            base.Awake();
            newGameButton = GameObject.Find("NewGame").GetComponent<MenuButton>();
            continueButton = GameObject.Find("Continue").GetComponent<MenuButton>();
            exitButton = GameObject.Find("Exit").GetComponent<MenuButton>();
        }
        
        void Start()
        {
            exitButton.OnClickEvent += OnExitClick;
            exitButton.gameObject.SetActive(Application.platform != RuntimePlatform.WebGLPlayer);

            continueButton.OnClickEvent += OnContinueClick;
            continueButton.gameObject.SetActive(SaveSystem.instance.HaveSave);

            newGameButton.OnClickEvent += OnNewGameClick;
        }

        public void OnNewGameClick()
        {
            SaveSystem.instance.playerData = SaveSystem.instance.GetDefaultData;
            SaveSystem.instance.Save();
            LoadManager.instance.LoadScene(GameSceneName);
        }

        public void OnContinueClick()
        {
            SaveSystem.instance.Load();
            LoadManager.instance.LoadScene(GameSceneName);
        }

        public void OnExitClick()
        {
            Application.Quit();
        }
    }
}