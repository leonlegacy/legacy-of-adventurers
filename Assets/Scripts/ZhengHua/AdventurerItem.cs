using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ZhengHua
{
    /// <summary>
    /// Word Space 上的傭兵物件
    /// </summary>
    public class AdventurerItem : MonoBehaviour
    {
        /// <summary>
        /// 傭兵大頭貼
        /// </summary>
        public SpriteRenderer head;
        /// <summary>
        /// 男性身體
        /// </summary>
        public SpriteRenderer maleBody;
        /// <summary>
        /// 女性身體
        /// </summary>
        public SpriteRenderer femaleBody;
        /// <summary>
        /// 死亡圖片
        /// </summary>
        public SpriteRenderer deadImage;

        private int _fullHp = 1;
        private int _nowHp = 0;
        
        public TMP_Text hpText;
        public Image hpBar;

        private bool _isDead;
        
        private bool _isMale;

        public void Awake()
        {
            Init();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void Init(int targetHp = 1, bool targetIsMale = false)
        {
            _fullHp = targetHp;
            Hp = _fullHp;
            isMale = targetIsMale;
        }

        /// <summary>
        /// 生命值
        /// </summary>
        public int Hp
        {
            get
            {
                return _nowHp;
            }
            set
            {
                _nowHp = value;
                hpText.text = $"{_nowHp}/{_fullHp}";
                hpBar.fillAmount = (float)_nowHp / _fullHp;
                isDead = _nowHp <= 0;
            }
        }

        /// <summary>
        /// 是否為男性
        /// </summary>
        public bool isMale
        {
            get
            {
                return _isMale;
            }
            private set
            {
                _isMale = value;
                maleBody.gameObject.SetActive(value && !_isDead);
                femaleBody.gameObject.SetActive(!value && !_isDead);
            }
        }

        /// <summary>
        /// 是否死亡
        /// </summary>
        public bool isDead
        {
            get
            {
                return _isDead;
            }
            private set
            {
                _isDead = value;
                maleBody.gameObject.SetActive(!value);
                femaleBody.gameObject.SetActive(!value);
                head.gameObject.SetActive(!value);
                deadImage.gameObject.SetActive(value);
            }
        }
    }
}