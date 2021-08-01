using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public class StartPanel : UIPanel
{
    [SerializeField] private AudioSource clickSound;
    
    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "Btn_Start":
                LoadGame();
                break;
        }
    }

    private void LoadGame()
    {
        clickSound.Play();
        UIPanel loadingPanel = UIManager.Instance.LoadUI(UILayer.TopLayer, "panel_loading", false);
        loadingPanel.OnShow();
        SceneMgr.Instance.LoadSceneAsync("level 0-1", () => { loadingPanel.OnClose(); });
    }
}
