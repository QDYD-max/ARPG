using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace CFramework
{
    public abstract class IBuff : MonoBehaviour
    {
        public BuffConfig config;
        
        protected UnityAction BuffOnTick;
        protected UnityAction BuffOnEnd;
        
        private IEnumerator CoBuffOnTick()
        {
            //Debug.Log("Buff开始触发");
            switch (config.trigger)
            {
                case BuffTriggerType.StartAndTick:
                    for (int i = 0; i < config.buffTimes; ++i)
                    {
                        yield return new WaitForSeconds(config.frequency);
                        BuffOnTick?.Invoke();
                    }
                    break;
                case BuffTriggerType.TickAndEnd:
                    BuffOnTick?.Invoke();
                    break;
                case BuffTriggerType.WaitThenTick:
                    yield return new WaitForSeconds(config.frequency);
                    for (int i = 0; i < config.buffTimes; ++i)
                    {
                        yield return new WaitForSeconds(config.frequency);
                        BuffOnTick?.Invoke();
                    }
                    break;
            }
            //Debug.Log("Buff结束");
            BuffOnEnd?.Invoke();
            Destroy(this);
            yield return null;
        }

        //buff特效播放，TODO
        private void PlayParticle()
        {
            
        }

        #region MonoBehaviour

        protected virtual void Start()
        {
            
        }

        protected virtual void OnEnable()
        {
            BuffOnTick += PlayParticle;
            StartCoroutine(CoBuffOnTick());
        }

        protected virtual void Update()
        {
            
        }

        protected virtual void OnDisable()
        {
            BuffOnTick -= PlayParticle;
            StopCoroutine(CoBuffOnTick());
        }

        #endregion

        #region OdinInspector

        

        #endregion
        
    }
}