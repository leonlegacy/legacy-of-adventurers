using UnityEngine;

namespace ZhengHua
{
    /// <summary>
    /// 單例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class SingtonMono<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static readonly object _lock = new object();
        protected static T _instance;
        public static T instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = FindObjectOfType<T>();
                        if (_instance == null)
                        {
                            GameObject obj = new GameObject(typeof(T).Name + "(Singleton)");
                            _instance = obj.AddComponent<T>();
                        }
                    }
                    return _instance;
                }
            }
        }

        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
            }
        }
    }
}