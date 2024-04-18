using System;
using UnityEngine;

namespace Utils
{
    public class ScriptableSingleton<T> : ScriptableObject where T : ScriptableObject
    {
        private static T _instance;

        protected ScriptableSingleton()
        {
            if (_instance != null)
                Debug.LogError("ScriptableSingleton already exists. Did you query the singleton in a constructor?");
            else
                _instance = (object)this as T;
        }

        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<T>($"{typeof(T).Name}");
                    if (_instance == null) throw new Exception($"No instance of {typeof(T).Name} found");
                }

                return _instance;
            }
        }

        protected virtual void OnValidate()
        {
            if (name != GetType().Name)
            {
                Debug.LogError($"{name} ScriptableSingleton must have same name as class {GetType().Name}");
            }
        }
    }
}