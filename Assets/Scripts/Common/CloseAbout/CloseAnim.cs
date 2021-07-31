using System;
using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public class CloseAnim : MonoBehaviour
{
    private RoleCtrl _curRoleCtrl;
    [SerializeField] private GameObject Attack01PS;
    [SerializeField] private GameObject Attack02PS;
    [SerializeField] private GameObject Attack03PS;
    private void Start()
    {
        _curRoleCtrl = GetComponent<RoleCtrl>();
    }

    public void OverAttack()
    {
        _curRoleCtrl.isAttack = false;
    }
    
    public void AttackDamage()
    {
        //AudioMgr.instance.PlaySound("Hit");
        float AttackDistance = 5;

        // 扇形的角度
        float AttackAngle = 140;
        // 玩家正前方的向量
        Vector3 norVec = transform.rotation * Vector3.forward;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AttackDistance, 1 << LayerMask.NameToLayer($"Role"));
        //Debug.Log(hitColliders.Length);
        if (hitColliders.Length != 0)
        {
            foreach (var hitCollider in hitColliders)
            {
                if(hitCollider!= transform.GetComponent<CharacterController>())
                {
                    // 玩家与敌人的方向向量
                    Vector3 temVec = hitCollider.gameObject.transform.position - transform.position;

                    float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;
                    if (angle <= AttackAngle * 0.5f)
                    {
                        Debug.Log("伤害"+ hitCollider.name);
                        EventCenter.Instance.EventTrigger<RoleBattle>(CEventType.RoleBattle, new RoleBattle()
                        {
                            Attacker = gameObject,
                            DamagedEntity = hitCollider.gameObject,
                            value = -10
                        });
                    }
                }
            }
        }
    }

    public void PlayAttack01PS()
    {
        Attack01PS.GetComponent<ParticleSystem>().Play();
    }

    public void PlayAttack02PS()
    {
        Attack02PS.GetComponent<ParticleSystem>().Play();
    }

    public void PlayAttack03PS()
    {
        Attack03PS.GetComponent<ParticleSystem>().Play();
    }
    
}
