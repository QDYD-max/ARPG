using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;
using UnityEngine.Events;

public class TestData : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NumericComponent numeric =  gameObject.AddComponent<NumericComponent>();;

        EventCenter.Instance.AddEventListener<NumericChange>(CEventType.NumericChange, DebugHP);
        numeric.Initialize(NumericType.MaxHp);
        numeric.Set(NumericType.MaxHpBase, 100);
        numeric.Set(NumericType.MaxHpAdd, 200);
        numeric.Set(NumericType.MaxHpFinalPct, 100f);
        //numeric.OnUpdate(NumericType.HpBase);
    }

    private void DebugHP(NumericChange change)
    {
        Debug.Log(change.Entity.name);
        Debug.Log("Type:" + change.NumericType);
        Debug.Log("Old:" + change.Old);
        Debug.Log("New:" + change.New);
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}