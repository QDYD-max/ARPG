using System;
using CFramework;
using UnityEngine.UI;

public class SettingPanel : UIPanel
{
    private void Start()
    {
    }

    protected override void OnClick(string btnName)
    {
        switch (btnName)
        {
            case "xbtn":
                OnClose();
                break;
        }
    }

    protected override void OnValueChanged(string toggleName, bool value)
    {
    }
}