using System;
using System.Collections;
using CFramework;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OnLoadingUI : UIPanel
{
    private Slider _progressSlider;
    private float _curProgress;
    private float _targetProgress;
    private AsyncOperation _async;
    private void Start()
    {
        _curProgress = 0;
        _progressSlider = GetControl<Slider>("slider_progress");
        EventCenter.Instance.AddEventListener<float>("Loading", ChangeProgressValue);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _progressSlider.value += 0.01f;
        }
    }

    private void ChangeProgressValue(float val)
    {
        _progressSlider.value = val;
    }
}