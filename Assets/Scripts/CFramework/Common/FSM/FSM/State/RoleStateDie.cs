using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateDie : RoleStateAbstract
{
    public RoleStateDie(RoleFSMMgr roleFsm) : base(roleFsm)
    {
    }

    /// <summary>
    /// 实现 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        
        CurRoleFSM.CurRoleCtrl.Animator.SetInteger(RoleAnimatorCondition.ToDie.ToString(), 1);
    }

    /// <summary>
    /// 实现 状态进行
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (CurAnimatorStateInfo.IsName(RoleAnimatorName.Death_01.ToString()))
        {
            
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
