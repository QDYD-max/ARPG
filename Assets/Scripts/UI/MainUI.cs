using System;
using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;
using UnityEngine.UI;

public class MainUI : UIPanel
{
    /// <summary>
    /// 主界面Hp UI
    /// </summary>
    private Text textMainPlayerHp;
    private Slider sliderMainPlayerHp;
    
    /// <summary>
    /// Hp属性
    /// </summary>
    private int _curHp;
    private int _curMaxHp;
    
    /// <summary>
    /// 主界面Mp UI
    /// </summary>
    private Text textMainPlayerShield;
    private Slider sliderMainPlayerShield;
    
    /// <summary>
    /// Mp属性
    /// </summary>
    private int _curShield;
    private int _curMaxShield;
    
    /// <summary>
    /// 主界面Energy UI
    /// </summary>
    private Text textMainPlayerEnergy;
    private Slider sliderMainPlayerEnergy;
    
    /// <summary>
    /// Energy属性
    /// </summary>
    private int _curEnergy;
    private int _curMaxEnergy;

    /// <summary>
    /// 技能1
    /// </summary>
    private Image imageSkill1Cd;
    private int _curSkill1Cd;
    private int _curMaxSkill1Cd;
    
    /// <summary>
    /// 技能2
    /// </summary>
    private Image imageSkill2Cd;
    private int _curSkill2Cd;
    private int _curMaxSkill2Cd;
    
    /// <summary>
    /// 技能3
    /// </summary>
    private Image imageSkill3Cd;
    private int _curSkill3Cd;
    private int _curMaxSkill3Cd;

    private void Start()
    {
        textMainPlayerHp = GetControl<Text>("Text_MainPlayerHp");
        sliderMainPlayerHp = GetControl<Slider>("Slider_MainPlayerHp");
        
        textMainPlayerShield = GetControl<Text>("Text_MainPlayerShield");
        sliderMainPlayerShield = GetControl<Slider>("Slider_MainPlayerShield");
        
        textMainPlayerEnergy = GetControl<Text>("Text_MainPlayerEnergy");
        sliderMainPlayerEnergy = GetControl<Slider>("Slider_MainPlayerEnergy");

        imageSkill1Cd = GetControl<Image>("Image_Skill1Cd");
        imageSkill2Cd = GetControl<Image>("Image_Skill2Cd");
        imageSkill3Cd = GetControl<Image>("Image_Skill3Cd");

        #region 判空

#if UNITY_EDITOR  
        if (textMainPlayerHp == null)
        {
            Debug.LogError(textMainPlayerHp.name + " missing");
        }
        if (sliderMainPlayerHp == null)
        {
            Debug.LogError(sliderMainPlayerHp.name + " missing");
        }
        if (textMainPlayerShield == null)
        {
            Debug.LogError(textMainPlayerShield.name + " missing");
        }
        if (sliderMainPlayerShield == null)
        {
            Debug.LogError(sliderMainPlayerShield.name + " missing");
        }
        if (textMainPlayerEnergy == null)
        {
            Debug.LogError(textMainPlayerEnergy.name + " missing");
        }
        if (sliderMainPlayerEnergy == null)
        {
            Debug.LogError(sliderMainPlayerEnergy.name + " missing");
        }
        if (imageSkill1Cd == null)
        {
            Debug.LogError(imageSkill1Cd.name + " missing");
        }
        if (imageSkill2Cd == null)
        {
            Debug.LogError(imageSkill2Cd.name + " missing");
        }
        if (imageSkill3Cd == null)
        {
            Debug.LogError(imageSkill3Cd.name + " missing");
        }
#endif

        #endregion

    }

    public override void OnInit()
    {
        EventCenter.Instance.AddEventListener<NumericChange>(CEventType.NumericChange, UpdateMainPlayerHp);
        EventCenter.Instance.AddEventListener<NumericChange>(CEventType.NumericChange, UpdateMainPlayerShield);
        EventCenter.Instance.AddEventListener<NumericChange>(CEventType.NumericChange, UpdateMainPlayerEnergy);
        
        EventCenter.Instance.AddEventListener<NumericChange>(CEventType.NumericChange, UpdateSkill1Cd);
        EventCenter.Instance.AddEventListener<NumericChange>(CEventType.NumericChange, UpdateSkill2Cd);
        EventCenter.Instance.AddEventListener<NumericChange>(CEventType.NumericChange, UpdateSkill3Cd);
    }

    private void UpdateSkill1Cd(NumericChange change)
    {
        if (change.Entity != MainPlayer.Instance.mainPlayerCtrl.gameObject) return;
        switch (change.NumericType)
        {
            case NumericType.SkillCd1:
                _curSkill1Cd = (int) change.New;
                break;
            case NumericType.MaxSkillCd1:
                _curMaxSkill1Cd = (int) change.New;
                break;
            default:
                //不是相关值计算直接返回
                return;
        }
        float percentVal = _curSkill1Cd * 1.0f / _curMaxSkill1Cd;
        if (float.IsNaN(percentVal) || float.IsInfinity(percentVal))
        {
            return;
        }

        percentVal = (float) Math.Round(percentVal, 4);

        imageSkill1Cd.fillAmount = percentVal;
        
    }
    
    private void UpdateSkill2Cd(NumericChange change)
    {
        if (change.Entity != MainPlayer.Instance.mainPlayerCtrl.gameObject) return;
        switch (change.NumericType)
        {
            case NumericType.SkillCd2:
                _curSkill2Cd = (int) change.New;
                break;
            case NumericType.MaxSkillCd2:
                _curMaxSkill2Cd = (int) change.New;
                break;
            default:
                //不是相关值计算直接返回
                return;
        }
        float percentVal = _curSkill2Cd * 1.0f / _curMaxSkill2Cd;
        if (float.IsNaN(percentVal) || float.IsInfinity(percentVal))
        {
            return;
        }

        percentVal = (float) Math.Round(percentVal, 4);

        imageSkill2Cd.fillAmount = percentVal;
        
    }
    
    private void UpdateSkill3Cd(NumericChange change)
    {
        if (change.Entity != MainPlayer.Instance.mainPlayerCtrl.gameObject) return;
        switch (change.NumericType)
        {
            case NumericType.SkillCd3:
                _curSkill3Cd = (int) change.New;
                break;
            case NumericType.MaxSkillCd3:
                _curMaxSkill3Cd = (int) change.New;
                break;
            default:
                //不是相关值计算直接返回
                return;
        }
        float percentVal = _curSkill3Cd * 1.0f / _curMaxSkill3Cd;
        if (float.IsNaN(percentVal) || float.IsInfinity(percentVal))
        {
            return;
        }

        percentVal = (float) Math.Round(percentVal, 4);

        imageSkill3Cd.fillAmount = percentVal;
    }
    
    private void UpdateMainPlayerHp(NumericChange change)
    {
        if (change.Entity != MainPlayer.Instance.mainPlayerCtrl.gameObject) return;
        switch (change.NumericType)
        {
            case NumericType.Hp:
                _curHp = (int) change.New;
                break;
            case NumericType.MaxHp:
                _curMaxHp = (int) change.New;
                break;
            default:
                //不是相关值计算直接返回
                return;
        }
        float percentVal = _curHp * 1.0f / _curMaxHp;
        if (float.IsNaN(percentVal) || float.IsInfinity(percentVal))
        {
            return;
        }

        percentVal = (float) Math.Round(percentVal, 4);
        
        sliderMainPlayerHp.value = percentVal;
        textMainPlayerHp.text = $"{_curHp}";
    }
    
    private void UpdateMainPlayerShield(NumericChange change)
    {
        if (change.Entity != MainPlayer.Instance.mainPlayerCtrl.gameObject) return;
        switch (change.NumericType)
        {
            case NumericType.Shield:
                _curShield = (int) change.New;
                break;
            case NumericType.MaxShield:
                _curMaxShield = (int) change.New;
                break;
            default:
                //不是相关值计算直接返回
                return;
        }
        float percentVal = _curShield * 1.0f / _curMaxShield;
        if (float.IsNaN(percentVal) || float.IsInfinity(percentVal))
        {
            return;
        }

        percentVal = (float) Math.Round(percentVal, 4);

        sliderMainPlayerShield.value = percentVal;
        textMainPlayerShield.text = $"{_curShield}";
    }
    
    private void UpdateMainPlayerEnergy(NumericChange change)
    {
        if (change.Entity != MainPlayer.Instance.mainPlayerCtrl.gameObject) return;
        switch (change.NumericType)
        {
            case NumericType.Energy:
                _curEnergy = (int) change.New;
                break;
            case NumericType.MaxEnergy:
                _curMaxEnergy = (int) change.New;
                break;
            default:
                //不是相关值计算直接返回
                return;
        }

        float percentVal = _curEnergy * 1.0f / _curMaxEnergy;
        if (float.IsNaN(percentVal) || float.IsInfinity(percentVal))
        {
            return;
        }

        percentVal = (float) Math.Round(percentVal, 4);
        
        sliderMainPlayerEnergy.value = percentVal;
        textMainPlayerEnergy.text = $"{_curEnergy}/{_curMaxEnergy}";
    }
    
}