using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 角色有限状态机管理
/// </summary>
public class RoleFSMMgr 
{
    /// <summary>
    /// 当前角色
    /// </summary>
    public RoleCtrl CurRoleCtrl { get; private set; }

    /// <summary>
    /// 当前角色枚举
    /// </summary>
    public RoleState CurRoleState { get; private set; }

    /// <summary>
    /// 当前角色状态
    /// </summary>
    private RoleStateAbstract m_CurrRoleState = null;

    private Dictionary<RoleState, RoleStateAbstract> m_RoleStateDic;

    public RoleFSMMgr(RoleCtrl roleCtrl)
    {
        CurRoleCtrl = roleCtrl;
        m_RoleStateDic = new Dictionary<RoleState, RoleStateAbstract>
        {
            [RoleState.Idle] = new RoleStateIdle(this),
            [RoleState.Run] = new RoleStateRun(this),
            [RoleState.Hurt] = new RoleStateHurt(this),
            [RoleState.Die] = new RoleStateDie(this)
        };

        if (m_RoleStateDic.ContainsKey(CurRoleState))
        {
            m_CurrRoleState = m_RoleStateDic[CurRoleState];
        }
    }

    public void OnUpdate()
    {
        if(m_CurrRoleState != null)
        {
            m_CurrRoleState.OnUpdate();
        }
    }

    public RoleStateAbstract GetRoleState(RoleState state)
    {
        if(m_RoleStateDic.ContainsKey(state))
        {
            return m_RoleStateDic[state];
        }

        return null;
    }

    public void ChangeState(RoleState newState, int stateValue)
    {
        if (newState == CurRoleState) 
            return;

        //退出当前状态
        if (m_CurrRoleState != null)
            m_CurrRoleState.OnLeave();

        //更新状态枚举
        CurRoleState = newState;
        //更新状态类型
        m_CurrRoleState = m_RoleStateDic[CurRoleState];
        
        //新状态进入
        m_CurrRoleState.curStateValue = stateValue;
        m_CurrRoleState.OnEnter();
    }
}
