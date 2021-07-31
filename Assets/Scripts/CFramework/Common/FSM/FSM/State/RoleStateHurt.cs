using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleStateHurt : RoleStateAbstract
{
    public RoleStateHurt(RoleFSMMgr roleFsm) : base(roleFsm)
    {
    }

    /// <summary>
    /// 实现 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
        CurRoleFSM.CurRoleCtrl.curAnimator.SetInteger(RoleAnimatorCondition.ToHurt.ToString(), curStateValue);
    }

    /// <summary>
    /// 实现 状态进行
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        if (MainPlayer.Instance.isAttack == false)
        {
            CurRoleFSM.ChangeState(RoleState.Idle, 0);
        }
    }

    /// <summary>
    /// 实现 离开状态
    /// </summary>
    public override void OnLeave()
    {
        base.OnLeave();
        CurRoleFSM.CurRoleCtrl.curAnimator.SetInteger(RoleAnimatorCondition.ToHurt.ToString(), 0);
    }
}