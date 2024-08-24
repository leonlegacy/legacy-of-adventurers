using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZhengHua
{
    public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        protected TMP_Text buttonText;
        protected string oText;

        public Action OnClickEvent;
        public Action OnEnterEvent;
        public Action OnExitEvent;

        [SerializeField]
        protected Color normalColor = Color.white;
        [SerializeField]
        protected Color hoverColor = Color.white;
        [SerializeField]
        protected Color pressedColor = Color.white;

        public virtual void Awake()
        {
            buttonText = GetComponentInChildren<TMP_Text>();
            oText = buttonText.text;
        }

        public virtual void Start()
        {
            buttonText.text = "  " + oText;
            buttonText.color = normalColor;
        }

        /// <summary>
        /// 按鈕點擊事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClickEvent?.Invoke();
            buttonText.color = pressedColor;
        }

        /// <summary>
        /// 滑鼠移入事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerEnter(PointerEventData eventData)
        {
            buttonText.text = "* " + oText;
            buttonText.color = hoverColor;
            OnEnterEvent?.Invoke();
        }

        /// <summary>
        /// 滑鼠移出事件
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerExit(PointerEventData eventData)
        {
            buttonText.text = "  " + oText;
            buttonText.color = normalColor;
            OnExitEvent?.Invoke();
        }
    }
}