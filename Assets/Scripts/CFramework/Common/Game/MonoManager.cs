using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;
using UnityEngine.Events;

public class MonoManager : MonoSingleton<MonoManager>
{
    private event UnityAction updateEvent;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (updateEvent != null)
            updateEvent();
    }

    //为外部提供的 添加帧更新事件的函数
    public void AddUpdateListener(UnityAction func)
    {
        updateEvent += func;
    }

    //为外部提供的 移除帧更新事件的函数
    public void RemoveUpdateListener(UnityAction func)
    {
        updateEvent -= func;
    }
}
