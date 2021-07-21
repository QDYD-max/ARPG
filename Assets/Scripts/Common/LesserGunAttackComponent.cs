using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CFramework
{
    public class LesserGunAttackComponent : MonoBehaviour
    {
        //-----镭射枪内容
        private Animator _animator;
        private bool flag = false;
        GameObject effect;
        GameObject ballPrefab;
        public List<GameObject> ballList = new List<GameObject>();
        private Vector3 _gunPos;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            ballPrefab = Resources.Load<GameObject>("ball");
            GameObject effectPrefab = Resources.Load<GameObject>("LaserBeam");
            effect = GameObject.Instantiate(effectPrefab);
            effect.SetActive(false);
        }

        private void Update()
        {
        }

        private IEnumerator CoLesserGunAttack(RaycastHit hit)
        {
            GameObject entity = hit.collider.gameObject;
            while (flag)
            {
                yield return new WaitForSeconds(0.2f);
                EventCenter.Instance.EventTrigger<RoleBattle>(CEventType.RoleBattle, new RoleBattle()
                {
                    Attacker = gameObject,
                    DamagedEntity = entity,
                    value = -10
                });
                //从命中位置生成小球
                GameObject ballEntity = GameObject.Instantiate(ballPrefab,
                    hit.point + new Vector3(Random.Range(0, 0.4f), Random.Range(0, 0.4f), Random.Range(0, 0.4f)),
                    Quaternion.identity);
                ballEntity.GetComponent<Rigidbody>().AddExplosionForce(200f, hit.point, 2f);
                ballList.Add(ballEntity);
            }

            yield return null;
        }


        public void Execute()
        {
            _gunPos = transform.Find("GunPos").position;
            //--------------交互部分
            _animator.SetBool("ToIdle", false);
            _animator.SetInteger("ToAttack", 5);
            //--------------特效部分
            effect.transform.position = _gunPos; //有枪模型的话应该跟随枪 TODO
            effect.transform.forward = transform.forward;
            effect.SetActive(true);
            //------------程序判断部分
            /*Vector3 screenPos = new Vector3(_mousePos.x, _mousePos.y,
                Mathf.Abs(Camera.main.transform.position.z));
            
            Vector3 worldPos =
                Camera.main.ScreenToWorldPoint(screenPos);*/

            RaycastHit hit;
            if (Physics.Raycast(_gunPos, transform.forward, out hit,
                Mathf.Infinity, 1 << LayerMask.NameToLayer($"Enemy")))
            {
                flag = true;
                StartCoroutine(CoLesserGunAttack(hit));
                Debug.Log("Did Hit");
            }
            else
            {
                Debug.Log("Did not Hit");
            }
        }

        public void Cancle()
        {
            
            //-------松开结束镭射枪内容
            _animator.SetInteger("ToAttack", 0);
            _animator.SetBool("ToIdle", true);
            flag = false;
            effect.SetActive(false);
        }
    }
}