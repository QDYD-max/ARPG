using System;
using UnityEngine;
using UnityEngine.Events;

namespace CFramework
{
    public class GameManager : MonoSingleton<GameManager>
    {
        private event UnityAction updateEvent;

        private void Awake()
        {
            GameManager.Instance.OnStartUP();
            MainPlayer.Instance.OnStartUP();
        }

        // Update is called once per frame
        void Update()
        {
            if (updateEvent != null)
                updateEvent();
        }

        //为外部提供的 添加帧更新事件的函数
        public void AddUpdateListener(UnityAction func)
        {
            updateEvent += func;
        }

        //为外部提供的 移除帧更新事件的函数
        public void RemoveUpdateListener(UnityAction func)
        {
            updateEvent -= func;
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