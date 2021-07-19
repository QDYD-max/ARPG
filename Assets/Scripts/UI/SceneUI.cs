using System.Collections;
using System.Collections.Generic;
using CFramework;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SceneUI : UIPanel
{
    private Dictionary<GameObject, GameObject> _goHealthUIDic = new Dictionary<GameObject, GameObject>();

    private float _offsetY = 1.5f;

    private Transform SceneLayer;

    // Start is called before the first frame update
    void Start()
    {
        SceneLayer = UIManager.Instance.SceneLayer;
        EventCenter.Instance.AddEventListener<NumericChange>(CEventType.NumericChange, RoleHealthChanged);
        EventCenter.Instance.AddEventListener<RoleBattle>(CEventType.RoleBattle, RoleCtrlDamaged);
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var o in _goHealthUIDic)
        {
            GameObject entity = o.Key;
            GameObject healthUI = o.Value;
            //抬高血条
            //血条跟随 TODO
            
            healthUI.transform.position = entity.transform.position + new Vector3(0, _offsetY, 0);
            
        }
    }

    private void RoleCtrlDamaged(RoleBattle battle)//战斗产生飘血单独分发事件
    {
        if (battle.DamagedEntity == MainPlayer.Instance.mainPlayerCtrl.gameObject) return;
        
        //飘血
        //可以考虑使用对象池
        
        GameObject textPrefab = ResourceLoader.Load<GameObject>(ResourceType.UI, "Text_Flow", true);
        GameObject textFlow = Instantiate(textPrefab, SceneLayer);
        textFlow.transform.position = battle.DamagedEntity.transform.position + new Vector3(0,1,0);
        FlyTo(textFlow.GetComponent<Text>(), Color.red);
    }

    private void RoleHealthChanged(NumericChange change)//数据改变实时改变血条
    {
        if (change.Entity == MainPlayer.Instance.mainPlayerCtrl.gameObject) return;
        if (change.NumericType != NumericType.Hp && change.NumericType != NumericType.MaxHp) return;

        //直接拿numeric不浪费额外的空间
        NumericComponent numeric = change.Entity.GetComponent<NumericComponent>();
        float percentVal = numeric[NumericType.Hp] * 1.0f / numeric[NumericType.MaxHp];
        if (float.IsNaN(percentVal) || float.IsInfinity(percentVal))
        {
            return;
        }
        
        Slider hp;
        if (_goHealthUIDic.ContainsKey(change.Entity))
        {
            hp = _goHealthUIDic[change.Entity].GetComponent<Slider>();
        }
        else
        {
            GameObject hpPrefab = ResourceLoader.Load<GameObject>(ResourceType.UI, "Slider_Health");
            GameObject hpEntity = GameObject.Instantiate(hpPrefab, transform);
            _goHealthUIDic[change.Entity] = hpEntity;

            hp = hpEntity.GetComponent<Slider>();
            Debug.Log(change.Entity.name +　"初始化血条");
        }
        
        hp.value = percentVal;
    }

    public void FlyTo(Graphic graphic, Color color)
    {
        RectTransform rt = graphic.rectTransform;
        graphic.color = color;
        Sequence mySequence = DOTween.Sequence();
        Tweener move1 = rt.DOMoveY(rt.position.y + 0.1f, 0.5f);
        Tweener move2 = rt.DOMoveY(rt.position.y + 0.2f, 0.5f);
        Tweener alpha1 = graphic.DOColor(new Color(color.r, color.g, color.b, 1), 0.5f);
        Tweener alpha2 = graphic.DOColor(new Color(color.r, color.g, color.b, 0), 0.5f);
        mySequence.Append(move1);
        mySequence.Join(alpha1);
        mySequence.AppendInterval(1);
        mySequence.Append(move2);
        mySequence.Join(alpha2);
    }
}