using System;
using UnityEngine;

namespace Common
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        protected static T instance;

        //Returns the instance of this singleton.
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = (T) FindObjectOfType(typeof(T));

                    if (instance == null)
                    {
                        Debug.LogWarning($"Creating Singleton_{typeof(T)}");
                        return instance = new GameObject($"(Singleton){typeof(T)}").AddComponent<T>();
                    }
                }

                return instance;
            }
        }

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}