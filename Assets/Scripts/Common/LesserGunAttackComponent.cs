using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CFramework
{
    public class LesserGunAttackComponent : SkillComponent
    {
        //-----镭射枪内容
        //属性
        private const int Range = 6;

        //动画控制
        private Animator _animator;

        //状态控制
        private bool _isLesserAttack = false;

        //资源控制
        private GameObject _effectTrajectory;
        private GameObject _effectFlash;
        private GameObject _effectHit;
        private const string BallName = "ball";
        private const string TrajectoryName = "LaserBeam";
        private const string FlashName = "Flash";
        private const string HitName = "Hit";
        private LineRenderer _line;

        //技能逻辑控制
        public List<GameObject> ballList = new List<GameObject>();
        private Transform _gun;
        private Coroutine _coGun;
        private GameObject target;


        private void Awake()
        {
            _animator = GetComponent<Animator>();

            //----------初始化资源
            PoolManager.Instance.InitPool(BallName, ResourceType.Pool, 10, 10);

            GameObject effectPrefabTrajectory =
                ResourceLoader.Load<GameObject>(ResourceType.Pool, TrajectoryName, true);
            _effectTrajectory = GameObject.Instantiate(effectPrefabTrajectory);
            _effectTrajectory.SetActive(false);
            _line = _effectTrajectory.GetComponent<LineRenderer>();

            GameObject effectPrefabFlash = ResourceLoader.Load<GameObject>(ResourceType.Pool, FlashName, true);
            _effectFlash = GameObject.Instantiate(effectPrefabFlash);
            _effectFlash.SetActive(false);

            GameObject effectPrefabHit = ResourceLoader.Load<GameObject>(ResourceType.Pool, HitName, true);
            _effectHit = GameObject.Instantiate(effectPrefabHit);
            _effectHit.SetActive(false);
            //------------------

            _gun = transform.Find("GunPos");
        }

        private void Update()
        {
            if (_isLesserAttack)
            {
                //跟随枪的模型
                _effectTrajectory.transform.position = _gun.position;
                _effectFlash.transform.position = _gun.position;
                _effectFlash.transform.forward = _gun.transform.forward;

                //尝试索敌
                target = Find(Range);
                //特效朝向 1.无目标朝前 2.有目标锁定
                if (target != null)
                {
                    Vector3 path = target.transform.position - transform.position;
                    
                    RaycastHit hit;
                    if (Physics.Raycast(_gun.position, path, out hit,
                        Range, 1<<LayerMask.NameToLayer("Enemy")))
                    {
                        if (_coGun == null)
                        {
                            _coGun = StartCoroutine(CoLesserGunAttack(hit));
                        }
                        //弹道特效
                        _line.SetPosition(1, new Vector3(0, 0, hit.distance));
                        _effectTrajectory.transform.forward = path;

                        //命中特效
                        _effectHit.transform.position = hit.point;
                        _effectHit.transform.forward = -path;
                        //Debug.Log("Did Hit");
                    }
                    /*墙体之类的阻挡
                     else
                    {
                        _effectHit.SetActive(false);
                        if (_coGun != null)
                        {
                            StopCoroutine(_coGun);
                            _coGun = null;
                        }
                        //Debug.Log("Did not Hit");
                    }
                    */
                    
                }
                else
                {
                    _effectHit.SetActive(false);
                    if (_coGun != null)
                    {
                        StopCoroutine(_coGun);
                        _coGun = null;
                    }
                    _effectTrajectory.transform.forward = transform.forward;
                    _line.SetPosition(1, new Vector3(0, 0, Range));
                }
            }
        }

        private IEnumerator CoLesserGunAttack(RaycastHit hit)
        {
            GameObject entity = hit.collider.gameObject;
            _effectTrajectory.SetActive(true);
            while (_isLesserAttack)
            {
                EventCenter.Instance.EventTrigger<RoleBattle>(CEventType.RoleBattle, new RoleBattle()
                {
                    Attacker = gameObject,
                    DamagedEntity = entity,
                    value = -10
                });
                //命中生成小球
                Vector3 offset = new Vector3(Random.Range(-0.5f, 0.5f), Random.Range(-0.5f, 0.5f),
                    Random.Range(-0.5f, 0.5f));
                GameObject ballEntity = PoolManager.Instance.LoadGameObject(BallName, ResourceType.Pool);
                ballEntity.transform.position = hit.point + offset;
                ballEntity.GetComponent<Rigidbody>().AddForce( offset * 10,ForceMode.Impulse);
                ballList.Add(ballEntity);
                
                yield return new WaitForSeconds(0.2f);
            }

            yield return null;
        }


        public void Execute()
        {
            _isLesserAttack = true;
            _animator.SetBool("ToIdle", false);
            _animator.SetInteger("ToAttack", 5);

            _effectFlash.SetActive(true);
            _effectHit.SetActive(true);
        }

        public void Cancle()
        {
            //-------松开结束镭射枪内容
            if (_coGun != null)
            {
                StopCoroutine(_coGun);
                _coGun = null;
            }

            _animator.SetInteger("ToAttack", 0);
            _animator.SetBool("ToIdle", true);
            _isLesserAttack = false;
            target = null;
            _effectTrajectory.SetActive(false);
            _effectHit.SetActive(false);
            _effectFlash.SetActive(false);
        }

        private void OnDestroy()
        {
            Destroy(_effectTrajectory);
            Destroy(_effectHit);
            Destroy(_effectFlash);
            //PoolManager.Instance.Clear(BallName);
        }
    }
}