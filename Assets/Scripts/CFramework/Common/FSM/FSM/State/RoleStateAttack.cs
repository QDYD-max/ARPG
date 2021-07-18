using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateAttack : RoleStateAbstract
{
    public string nextState;

    public RoleStateAttack(RoleFSMMgr roleFsm) : base(roleFsm)
    {
    }

    /// <summary>
    /// 实现 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        CurRoleFSM.CurRoleCtrl.Animator.Play("Attack_0" + curStateValue);
    }

    /// <summary>
    /// 实现 状态进行
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (CurAnimatorStateInfo.normalizedTime > 0.99f)
        {
            CurRoleFSM.ChangeState(RoleState.Idle, 0);
        }
    }

    /// <summary>
    /// 实习 离开状态
    /// </summary>
    public override void OnLeave()
    {
        
        base.OnLeave();
    }
}