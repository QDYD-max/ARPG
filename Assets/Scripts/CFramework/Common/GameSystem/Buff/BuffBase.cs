using System;
using System.Collections;
using UnityEngine;

namespace CFramework
{
    public abstract class BuffBase : MonoBehaviour
    {
        public BuffType buffType;

        private CountDownTimer timer;
        public float lifeTime;
        public int frequency;

        private void Awake()
        {
            Init();
        }

        private void OnEnable()
        {
            timer = new CountDownTimer(lifeTime);
        }

        private void Update()
        {
            
        }

        private void OnDisable()
        {
            timer = null;
        }

        public abstract void Init();

        public abstract void OnEnter();

        public abstract void OnTick();

        public abstract void OnFinish();
    }
}