namespace ZhengHua
{
    /// <summary>
    /// 單例模式
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class Sington<T> where T : class
    {
        private static readonly object _lock = new object();
        private static T _instance;
        public static T instance
        {
            get
            {
                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = System.Activator.CreateInstance<T>();
                    }
                    return _instance;
                }
            }
        }
    }
}