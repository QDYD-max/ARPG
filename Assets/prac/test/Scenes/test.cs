using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public class test : MonoBehaviour
{
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
        if (Input.GetKeyUp(KeyCode.A))
        {
            UIManager.Instance.Clear();
            UIPanel loading = UIManager.Instance.LoadUI(UILayer.TopLayer, "panel_loading");
            loading.OnShow();
            SceneMgr.Instance.LoadSceneAsync("Scene2", null);
            //SceneMgr.Instance.LoadScene("Scene2", null);
        }
    }
}
