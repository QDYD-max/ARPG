using System;
using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Buff配置", menuName = "技能|状态/Buff配置")]
public class BuffConfig : ScriptableObject
{
    public int buffTimes = 5;
    public float frequency = 0.5f;
    public BuffType type;
    public BuffTriggerType trigger;
    public GameObject particle;

}
