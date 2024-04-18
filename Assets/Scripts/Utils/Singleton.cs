using UnityEngine;

namespace Utils
{
    public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        public static T Instance { get; private set; }

        public virtual void Awake()
        {
            if (Instance == null)
            {
                Instance = this as T;
            }
            else
            {
                Debug.Log("Should not create 2 instances of the same singleton");
                Destroy(gameObject);
            }
        }

        // clean up static instance
        protected virtual void OnDestroy()
        {
            if (Instance == this) Instance = null;
        }
    }
}