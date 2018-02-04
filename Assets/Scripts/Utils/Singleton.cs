using UnityEngine;

namespace Utils
{
    /// <summary>
    /// A singleton base class to easily implement self instantiating singletons
    /// </summary>
    /// <typeparam name="T">Is the type of the singleton class itself so it can return itself when instance is called</typeparam>
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public T Instance
        {
            get
            {
                if (_instance == null)
                {
                    Init();
                }
                return _instance;
            }
        }

        private T _instance;

        protected virtual void Init()
        {
            var go = new GameObject(typeof(T).Name + "Instance");
            _instance = go.AddComponent<T>();
        }

        private void Awake()
        {
            if (_instance == null)
            {
                Init();
            }
        }
    }
}
