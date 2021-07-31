using System;
using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public abstract class IRoleAI : MonoBehaviour
{
    public abstract void DoAI();
    [SerializeField] protected BehaviorTree tree;

}