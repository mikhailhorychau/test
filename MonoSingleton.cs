﻿using UnityEngine;

namespace UIScripts
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null)
                    _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    var obj = new GameObject($"[{typeof(T).Name}]");
                    _instance = obj.AddComponent<T>();
                    DontDestroyOnLoad(_instance);
                }

                return _instance;
            }
        }
    }
}