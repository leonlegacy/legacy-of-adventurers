using TMPro;
using UnityEngine;

namespace ZhengHua
{ 
    public class AdventurerCanvas : CanvasSingtonMono<AdventurerCanvas>
    {
        public MenuButton confirmButton;
        public Transform content;
        public GameObject prefab;

        public override void Awake()
        {
            base.Awake();

            foreach (Transform child in transform.GetComponentsInChildren<Transform>())
            {
                if (child.name == "Hire Button")
                    confirmButton = child.GetComponent<MenuButton>();
            }

            Hide();
        }

        public override void Show()
        {
            base.Show();

            foreach(var adv in AdvManager.instance.Candidates)
            {
                GameObject obj = Instantiate(prefab, content);
                obj.GetComponentInChildren<TMP_Text>().text = adv.Name;
            }
        }

        public override void Hide()
        {
            base.Hide();
            GameManager.instance.OnFirstEnterGameOnClick?.Invoke();
        }
    }
}