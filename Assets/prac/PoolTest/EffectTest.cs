using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public class EffectTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PoolManager.Instance.LoadGameObject("Cube", ResourceType.Pool);
        PoolManager.Instance.LoadGameObject("Cube", ResourceType.Pool);
        PoolManager.Instance.LoadGameObject("Cube", ResourceType.Pool);
        PoolManager.Instance.LoadGameObject("Cube", ResourceType.Pool);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
