using System;
using CFramework;
using UnityEngine;


public class BuffTest : BuffBase
{
    private void Update()
    {
        
    }

    public override void Init()
    {
        
    }

    public override void OnEnter()
    {
    }

    public override void OnTick()
    {
        NumericComponent numeric = this.gameObject.GetComponent<NumericComponent>();
        int hp = numeric.GetAsInt(NumericType.Hp);
        numeric.Set(NumericType.HpBase, hp - 1);
        Debug.Log(numeric.GetAsInt(NumericType.Hp));
    }

    public override void OnFinish()
    {
    }
    
}