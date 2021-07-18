namespace CFramework
{
    public enum NumericType
    {
        //value均为1000+n,value计算属性均大于max
        //base add都为整型
        //pct 为float 单位为百分比%
        //先用着,虽然有的地方不合理
        Max = 10000,
        
        //速度
        Speed = 1000,
        SpeedBase = Speed * 10 + 1,
        SpeedAdd = Speed * 10 + 2,
        SpeedPct = Speed * 10 + 3,
        SpeedFinalAdd = Speed * 10 + 4,
        SpeedFinalPct = Speed * 10 + 5,
        
        //生命值
        Hp = 1001,
        HpBase = Hp * 10 + 1,
        
        //最大生命值
        MaxHp = 1002,
        MaxHpBase = MaxHp * 10 + 1,
        MaxHpAdd = MaxHp * 10 + 2,
        MaxHpPct = MaxHp * 10 + 3,
        MaxHpFinalAdd = MaxHp * 10 + 4,
        MaxHpFinalPct = MaxHp * 10 + 5,
        
        //护盾值
        Shield = 1003,
        ShieldBase = Shield * 10 + 1,

        //最大护盾值
        MaxShield = 1004,
        MaxShieldBase = MaxShield * 10 + 1,
        MaxShieldAdd = MaxShield * 10 + 2,
        MaxShieldPct = MaxShield * 10 + 3,
        MaxShieldFinalAdd = MaxShield * 10 + 4,
        MaxShieldFinalPct = MaxShield * 10 + 5,
        
        //能量值
        Energy = 1005,
        EnergyBase = Energy * 10 + 1,
        
        //最大能量值
        MaxEnergy = 1006,
        MaxEnergyBase = MaxEnergy * 10 + 1,
        MaxEnergyAdd = MaxEnergy * 10 + 2,
        MaxEnergyPct = MaxEnergy * 10 + 3,
        MaxEnergyFinalAdd = MaxEnergy * 10 + 4,
        MaxEnergyFinalPct = MaxEnergy * 10 + 5,
        
        //技能Cd
        SkillCd1 = 1007,
        SkillCd1Base = SkillCd1 * 10 + 1,
        
        MaxSkillCd1 = 1008,
        MaxSkillCd1Base = MaxSkillCd1 * 10 + 1,
        
        SkillCd2 = 1009,
        SkillCd2Base = SkillCd2 * 10 + 1,
        
        MaxSkillCd2 = 1010,
        MaxSkillCd2Base = MaxSkillCd2 * 10 + 1,
        
        SkillCd3 = 1011,
        SkillCd3Base = SkillCd3 * 10 + 1,
        
        MaxSkillCd3 = 1012,
        MaxSkillCd3Base = MaxSkillCd3 * 10 + 1,
    }
}