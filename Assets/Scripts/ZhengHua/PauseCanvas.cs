using UnityEngine;

namespace ZhengHua
{ 
    public class PauseCanvas : CanvasSingtonMono<PauseCanvas>
    {
        [SerializeField]
        private string MenuSceneName = "MenuScene";

        public MenuButton backMenuButton;
        public MenuButton continueButton;

        public override void Awake()
        {
            base.Awake();
            foreach (Transform child in transform.GetComponentsInChildren<Transform>())
            {
                if(child.name == "BackMenu")
                    backMenuButton = child.GetComponent<MenuButton>();
                else if (child.name == "Continue")
                    continueButton = child.GetComponent<MenuButton>();
            }
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