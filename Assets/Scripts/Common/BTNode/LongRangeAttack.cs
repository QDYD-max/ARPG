using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public class LongRangeAttack : ActionNode
{

    [SerializeField] private string attackName = "Attack0";
    
    private RoleCtrl _curRoleCtrl;
    protected override void OnStart()
    {
        _curRoleCtrl = entity.GetComponent<RoleCtrl>();
    }

    protected override NodeState OnUpdate()
    {
        if (!_curRoleCtrl.isAttack)
        {
            _curRoleCtrl.direction = Vector3.zero;
            _curRoleCtrl.isAttack = true;
            _curRoleCtrl.curAnimator.Play(attackName);
            Debug.Log(entity.name + "发射");
            return NodeState.Success;
        }
        return NodeState.Running;
    }

    protected override void OnStop()
    {
        Debug.Log(entity.name + "攻击结束");
    }
}
