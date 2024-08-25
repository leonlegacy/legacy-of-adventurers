using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ZhengHua
{
    public class AdventurerHandle : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        /// <summary>
        /// 姓名文字
        /// </summary>
        public TMP_Text nameText;
        /// <summary>
        /// 血量文字
        /// </summary>
        public TMP_Text hpText;
        /// <summary>
        /// 傭金文字
        /// </summary>
        public TMP_Text costText;
        /// <summary>
        /// 財產文字
        /// </summary>
        public TMP_Text legacyText;
        /// <summary>
        /// 僱傭文字
        /// </summary>
        public TMP_Text hireText;
        private int hireCost = 0;
        /// <summary>
        /// 傭兵大頭貼
        /// </summary>
        public Image head;

        /// <summary>
        /// 點擊按鈕事件
        /// </summary>
        public Action OnClickEvent;
        /// <summary>
        /// 滑鼠移入事件
        /// </summary>
        public Action OnEnterEvent;
        /// <summary>
        /// 滑鼠移出事件
        /// </summary>
        public Action OnExitEvent;
        /// <summary>
        /// 選擇改變事件
        /// </summary>
        public Action<Adventurer> OnSelectedChangeEvent;
        public Adventurer _data;

        [SerializeField]
        protected Color normalColor = Color.white;
        [SerializeField]
        protected Color hoverColor = Color.white;
        [SerializeField]
        protected Color pressedColor = Color.white;

        private bool _isSelected = false;
        public bool isSelected 
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (GameManager.instance.ParayIsFull)
                {
                    return;
                }
                _isSelected = value;
                _data.IsSelected = _isSelected;
                hireText.text = _isSelected ? "已僱傭" : "僱傭";
                OnSelectedChangeEvent?.Invoke(_data);
                AdvManager.instance.SelectCandidate(Array.IndexOf(AdvManager.instance.Candidates, _data));
            }
        }

        public void SetData(Adventurer data)
        {
            _data = data;
            nameText.text = data.Name;
            hpText.text = $"生命: {data.Health}";
            costText.text = $"傭金: {data.Cost}";
            legacyText.text = $"財產: {data.Legacy}";
            hireText.text = data.IsSelected ? "已僱傭" : "僱傭";
            head.sprite = data.avatar;
            hireCost = data.Cost;
        }

        /// <summary>
        /// 按鈕點擊事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke();
            isSelected = !isSelected;
        }

        /// <summary>
        /// 滑鼠移入事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            OnEnterEvent?.Invoke();
        }

        /// <summary>
        /// 滑鼠移出事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            OnExitEvent?.Invoke();
        }
    }
}