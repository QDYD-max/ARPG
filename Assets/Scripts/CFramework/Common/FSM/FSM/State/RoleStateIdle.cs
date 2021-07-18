using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateIdle : RoleStateAbstract
{
    public RoleStateIdle(RoleFSMMgr roleFsm) : base(roleFsm)
    {
    }

    /// <summary>
    /// 实现 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        CurRoleFSM.CurRoleCtrl.Animator.SetBool(RoleAnimatorCondition.ToIdle.ToString(), true);
    }

    /// <summary>
    /// 实现 状态进行
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (CurAnimatorStateInfo.IsName(RoleAnimatorName.Idle.ToString()))
        {
            
        }
    }

    /// <summary>
    /// 实习 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        CurRoleFSM.CurRoleCtrl.Animator.SetBool(RoleAnimatorCondition.ToIdle.ToString(), false);
    }
}
