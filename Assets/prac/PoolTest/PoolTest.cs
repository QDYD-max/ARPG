using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;
using UnityEngine.Pool;

public class PoolTest : MonoBehaviour
{
    private Stack<GameObject> _stack = new Stack<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        PoolManager.Instance.InitPool("Cube", ResourceType.Pool, 5, 5);
        //PoolManager.Instance.LoadGameObject("Cube", ResourceType.Pool);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    void OnGUI()
    {
        if (GUILayout.Button("Create Particles"))
        {
            _stack.Push(PoolManager.Instance.LoadGameObject("Cube", ResourceType.Pool));
        }
        
        if (GUILayout.Button("Release Particles"))
        {
            PoolManager.Instance.ReleaseGameObject(_stack.Pop(), "Cube");
        }
    }
}