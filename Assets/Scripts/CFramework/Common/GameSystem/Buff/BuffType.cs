namespace CFramework
{
    //Buff增益还是减益
    public enum BuffType
    {
        DeBuff,
        Buff
    }

    public enum BuffTriggerType
    {
        //Buff开始立即执行
        StartAndTick,
        //Buff执行然后立马结束
        TickAndEnd,
        //等待一个间隔再开始执行Buff
        WaitThenTick
    }
}