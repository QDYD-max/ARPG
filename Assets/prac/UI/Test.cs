using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;
using UnityEngine.Pool;

public class Test : MonoBehaviour
{
    public Transform UIRoot;

    // Start is called before the first frame update
    void Start()
    {
        UIManager.Instance.OnStartUP();
        UIPanel main = UIManager.Instance.LoadUI(UILayer.MainLayer, "background");
        main.OnShow();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
