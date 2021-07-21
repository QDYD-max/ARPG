using System.Collections;
using CFramework;
using UnityEngine;

namespace CFramework
{
    public class SwordSkillComponent : MonoBehaviour
    {
        private Coroutine _coBurningScope;
        
        //领域展开
        private IEnumerator BurningScope()
        {
            for (int i = 0; i < 10; ++i)
            {
                Collider[] colliders = Physics.OverlapSphere(transform.position, 3f, 1 << LayerMask.NameToLayer($"Enemy"));
                foreach (var collider in colliders)
                {
                    EventCenter.Instance.EventTrigger<RoleBattle>(CEventType.RoleBattle, new RoleBattle()
                    {
                        Attacker = gameObject,
                        DamagedEntity = collider.gameObject,
                        value = -10
                    });
                    //从命中位置生成小球
                    //GameObject ballEntity = GameObject.Instantiate(ballPrefab, collider.transform);
                }

                yield return new WaitForSeconds(1f);
            }

            yield return null;
        }

        public void Execute()
        {
            _coBurningScope = StartCoroutine(BurningScope());
        }

        public void Stop()
        {
            if (_coBurningScope == null) return;
            StopCoroutine(_coBurningScope);
        }
    }
}