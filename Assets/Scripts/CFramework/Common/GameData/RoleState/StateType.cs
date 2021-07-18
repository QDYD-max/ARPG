namespace CFramework
{
    public enum StateType
    {
        /// <summary>
        /// 攻击状态
        /// </summary>
        IsAttack,
        
        /// <summary>
        /// 攻击->连击状态
        /// </summary>
        IsAttack1,
        IsAttack2,
        IsAttack3,
        
        /// <summary>
        /// 小技能状态
        /// </summary>
        IsSkill1,
        /// <summary>
        /// 大招状态
        /// </summary>
        IsSkill2,
        
        /// <summary>
        /// 移动状态
        /// </summary>
        IsMove,
        
        /// <summary>
        /// 冲刺状态
        /// </summary>
        IsDodge,
        
    }
}