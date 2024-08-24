using UnityEngine;

namespace ZhengHua
{ 
    public class FirstEnterGameCanvas : CanvasSingtonMono<FirstEnterGameCanvas>
    {
        public MenuButton hideButton;

        public override void Awake()
        {
            base.Awake();
            hideButton = transform.GetComponentInChildren<MenuButton>();
        }
        
        public void Start()
        {
            hideButton.OnClickEvent += this.Hide;

            this.Hide();
        }

        public override void Hide()
        {
            base.Hide();
            GameManager.instance.OnFirstEnterGameOnClick?.Invoke();
        }
    }
}