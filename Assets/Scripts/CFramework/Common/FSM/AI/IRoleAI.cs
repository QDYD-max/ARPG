using System;
using System.Collections;
using System.Collections.Generic;
using CFramework.BT;
using UnityEngine;

public abstract class IRoleAI : MonoBehaviour
{
    public abstract void DoAI();

    private void Update()
    {
        DoAI();
    }
}