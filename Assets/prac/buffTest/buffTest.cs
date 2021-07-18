using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;
using UnityEngine.Events;

public class buffTest : IBuff
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        BuffOnTick += Damage;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        BuffOnTick -= Damage;
    }

    private void Damage()
    {
        Debug.Log(gameObject.name + "具体调用");
    }
}
