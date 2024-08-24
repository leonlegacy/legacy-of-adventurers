using UnityEngine;

namespace ZhengHua
{
    /// <summary>
    /// 畫布用的單例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CanvasSingtonMono<T> : SingtonMono<T> where T : MonoBehaviour
    {
        protected Canvas canvas;

        public override void Awake()
        {
            base.Awake();
            canvas = GetComponent<Canvas>();
        }

        /// <summary>
        /// 隱藏畫布
        /// </summary>
        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// 顯示畫布
        /// </summary>
        public void Show()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// 切換畫布顯示狀態
        /// </summary>
        /// <param name="force">強制開/關</param>
        public void Toggle(bool? force = null)
        {
            if(force ?? !gameObject.activeSelf)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }
    }
}