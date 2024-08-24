using UnityEngine;

namespace ZhengHua
{ 
    public class PauseCanvas : CanvasSingtonMono<PauseCanvas>
    {
        [SerializeField]
        private string MenuSceneName = "MenuScene";

        public MenuButton backMenuButton;
        public MenuButton continueButton;

        public GameState gameState = GameState.None;

        public override void Awake()
        {
            base.Awake();
            backMenuButton = GameObject.Find("BackMenu").GetComponent<MenuButton>();
            continueButton = GameObject.Find("Continue").GetComponent<MenuButton>();
        }
        
        public void Start()
        {
            backMenuButton.OnClickEvent += BackToMenu;
            continueButton.OnClickEvent += this.Hide;

            this.Hide();
        }

        public override void Hide()
        {
            base.Hide();
            continueButton.Start();
        }

        public void BackToMenu()
        {
            LoadManager.instance.LoadScene(MenuSceneName);
        }
    }
}