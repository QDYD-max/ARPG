using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#region SceneType 场景类型
/// <summary>
/// 场景类型
/// </summary>
public enum SceneType
{
    LogOn,
    City,
}
#endregion

#region WindowUIType 窗口UI类型
/// <summary>
/// 窗口UI类型
/// </summary>
public enum WindowUIType
{
    /// <summary>
    /// 无
    /// </summary>
    None,
    /// <summary>
    /// 登陆窗口
    /// </summary>
    LogOn,
    /// <summary>
    /// 注册窗口
    /// </summary>
    Reg,
}
#endregion

#region RoleType 角色类型
/// <summary>
/// 角色类型
/// </summary>
public enum RoleType
{
    /// <summary>
    /// 无
    /// </summary>
    None,
    /// <summary>
    /// 玩家
    /// </summary>
    Player,
    /// <summary>
    /// 怪物
    /// </summary>
    Monster,
}
#endregion

#region RoleState 角色状态
public enum RoleState
{
    /// <summary>
    /// 无
    /// </summary>
    None,
    /// <summary>
    /// 默认待机
    /// </summary>
    Idle,
    /// <summary>
    /// 跑
    /// </summary>
    Run,
    /// <summary>
    /// 攻击
    /// </summary>
    Attack,
    /// <summary>
    /// 受伤
    /// </summary>
    Hurt,
    /// <summary>
    /// 死亡
    /// </summary>
    Die,
    /// <summary>
    /// 潜行
    /// </summary>
    Crouch,
    /// <summary>
    /// 跳跃
    /// </summary>
    Jump,
}

#endregion

#region RoleAnimatorName 角色动画名称
public enum RoleAnimatorName
{
    Run,
    Idle,
    Jump,
    Attack_01,
    Attack_02,
    Attack_03,
    Attack_04,
    Attack_05,
    Attack_06,
    Walk_Weapon,
    Run_Weapon,
    Dame_01,
    Dame_02,
    Death_01,
    Death_02,
}
#endregion

#region

public enum RoleAnimatorCondition
{
    //每个整数值的状态0都为保留值，表示不在该状态
    ToAttack,
    ToIdle,
    ToDie,
    ToHurt,
    ToCrouch,
    ToJump,
    ToRun,
}
#endregion