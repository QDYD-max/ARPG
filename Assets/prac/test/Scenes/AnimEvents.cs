using System;
using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.InputSystem;

public class AnimEvents : MonoBehaviour
{
    [SerializeField] public AnimatorController AnimatorSword;
    [SerializeField] public AnimatorController AnimatorGun;
    private void Awake()
    {
        
    }

    public void AttackDamage()
    {
        //AudioMgr.instance.PlaySound("Hit");

        // 扇形的角度
        float AttackAngle = 140;
        // 玩家正前方的向量
        Vector3 norVec = transform.rotation * Vector3.forward;

        float AttackDistance = 10f;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AttackDistance, 1 << LayerMask.NameToLayer($"Enemy"));
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

    public void SkillDamage()
    {
        //AudioMgr.instance.PlaySound("Hit");
        

        float AttackDistance = 3f;
        // 扇形的角度
        float AttackAngle = 90;
        // 玩家正前方的向量
        Vector3 norVec = transform.rotation * Vector3.forward;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, AttackDistance, 1 << LayerMask.NameToLayer($"Enemy"));
        if (hitColliders.Length != 0)
        {
            
        }
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider != transform.GetComponent<CharacterController>())
            {
                // 玩家与敌人的方向向量
                Vector3 temVec = hitCollider.gameObject.transform.position - transform.position;

                float angle = Mathf.Acos(Vector3.Dot(norVec.normalized, temVec.normalized)) * Mathf.Rad2Deg;
                if (angle <= AttackAngle * 0.5f)
                {
                    EventCenter.Instance.EventTrigger<RoleBattle>(CEventType.RoleBattle, new RoleBattle()
                    {
                        Attacker = gameObject,
                        DamagedEntity = hitCollider.gameObject,
                        value = -20
                    });
                    Debug.Log("技能伤害"+ hitCollider.name);
                }
            }
        }
    }

    public void OverAttack()
    {
        MainPlayer.Instance.isAttack = false;
    }
}
