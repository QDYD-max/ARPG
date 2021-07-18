using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public abstract class RoleStateAbstract
{
    public RoleFSMMgr CurRoleFSM { get; private set; }

    protected AnimatorStateInfo CurAnimatorStateInfo { get; set; }

    public IRoleAI CurAI { get; private set; }

    public int curStateValue;

    public RoleStateAbstract(RoleFSMMgr roleFsm)
    {
        CurRoleFSM = roleFsm;
    }


    /// <summary>
    /// 进入状态
    /// </summary>
    public virtual void OnEnter()
    {
        CurAI = CurRoleFSM.CurRoleCtrl.curAI;
    }

    /// <summary>
    /// 状态进行
    /// </summary>
    public virtual void OnUpdate()
    {
        CurAnimatorStateInfo = CurRoleFSM.CurRoleCtrl.Animator.GetCurrentAnimatorStateInfo(0);
    }

    /// <summary>
    /// 离开状态
    /// </summary>
    public virtual void OnLeave()
    {

    }
}
