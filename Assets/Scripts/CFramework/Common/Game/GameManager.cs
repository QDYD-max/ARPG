using System;
using UnityEngine;
using UnityEngine.Events;

namespace CFramework
{
    public class GameManager : MonoSingleton<GameManager>
    {
        

        private void Awake()
        {
            
            MainPlayer.Instance.OnStartUP();
            MonoManager.Instance.OnStartUP();
        }
        
        public void Clear()
        {
            PoolManager.Instance.Clear();
            AudioManager.Instance.Clear();
            UIManager.Instance.Clear();
            ResourceLoader.Clear();
        }
    }
}