using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using CFramework;
using UnityEngine;

public class BuffReceiver : MonoBehaviour
{
    private void Awake()
    {
        NumericComponent numeric =  gameObject.AddComponent<NumericComponent>();
        numeric.Initialize(NumericType.Hp);
        numeric.Set(NumericType.HpBase, 1000);
        Debug.Log(numeric.GetAsInt(NumericType.Hp));
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private async void Waitasd()
    {
        await Task.Delay(500);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
