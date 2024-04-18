using UnityEngine;

namespace Utils
{
    public abstract class PersistentSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        public virtual void Awake ()
        {
            if (Instance == null) 
            {
                Instance = this as T;
                DontDestroyOnLoad (this);
            } 
            else 
            {
                Debug.Log("Should not create 2 instances of the same singleton: " + gameObject.name);
                Destroy(gameObject);
            }
        }

        protected void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}

