namespace CFramework
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;

    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        // Check to see if we're about to be destroyed.
        private static bool m_ShuttingDown = false;
        private static object m_Lock = new object();
        private static T m_Instance;

        /// <summary>
        /// Access singleton instance through this propriety.
        /// </summary>
        public static T Instance
        {
            get
            {
                if (m_ShuttingDown)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed. Returning null.");
                    return null;
                }


                if (m_Instance == null)
                {
                    // Search for existing instance.
                    m_Instance = (T) FindObjectOfType(typeof(T));

                    // Create new instance if one doesn't already exist.
                    if (m_Instance == null)
                    {
                        lock (m_Lock)
                        {
                            if (m_Instance == null)
                            {
                                // Need to create a new GameObject to attach the singleton to.
                                var singletonObject = new GameObject();
                                m_Instance = singletonObject.AddComponent<T>();
                                singletonObject.name = typeof(T).ToString() + " (Singleton)";

                                GameObject parent = GameObject.Find("Boot");
                                if (parent == null)
                                {
                                    parent = new GameObject("Boot");
                                    DontDestroyOnLoad(parent);
                                }

                                if (parent != null)
                                {
                                    singletonObject.transform.parent = parent.transform;
                                }

                                // Make instance persistent.
                                DontDestroyOnLoad(singletonObject);
                            }
                        }
                    }
                }

                return m_Instance;
            }
        }

        public void OnStartUP()
        {
            
        }
        
        private void OnApplicationQuit()
        {
            m_ShuttingDown = true;
        }


        private void OnDestroy()
        {
            m_ShuttingDown = true;
        }
    }


    public class Singleton<T> where T : class, new()
    {
        static T instance;

        private static readonly object _lock = new object();

        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (_lock)
                    {
                        if (instance == null)
                            instance = new T();
                    }
                }

                return instance;
            }
        }
        
        public void OnStartUP()
        {
            
        }
    }
}