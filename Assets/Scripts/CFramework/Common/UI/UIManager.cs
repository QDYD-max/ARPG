using System;
using System.Collections.Generic;
using System.Text;
using CFramework;
using UnityEngine;
using Object = UnityEngine.Object;

namespace CFramework
{
    public enum UILayer
    {
        //场景UI，一般在主界面UI之下场景之上
        SceneLayer = 0,
        //主界面UI，一般用户操作的界面就在这里
        MainLayer = 1000,
        NormalLayer = 2000,
        //广播UI，广播消息、跑马灯消息在这里
        InfoLayer = 3000,
        //提示UI，弹窗一般在这里，如：错误弹窗，网络连接弹窗
        TipLayer = 4000,
        //最上层UI，一般加载界面时覆盖所有UI
        TopLayer = 5000
    }

    public class UIManager : Singleton<UIManager>
    {
        //UI Panel View的栈
        public Stack<UIPanel> uiStack = new Stack<UIPanel>();
        
        //所有UI Panel的字典,每个UI Panel都是独立的
        private Dictionary<string, UIPanel> uiPaneDic = new Dictionary<string, UIPanel>();
        
        public readonly Transform SceneLayer;
        public readonly Transform MainLayer;
        public readonly Transform NormalLayer;
        public readonly Transform InfoLayer;
        public readonly Transform TipLayer;
        public readonly Transform TopLayer;

        public readonly RectTransform canvas;
        public readonly RectTransform canvas3D;

        public UIManager()
        {
            GameObject res = ResourceLoader.Load<GameObject>(ResourceType.UI, "UI");
            GameObject obj = Object.Instantiate(res);
            Object.DontDestroyOnLoad(obj);

            canvas = obj.transform.Find("Canvas").transform as RectTransform;
            canvas3D = obj.transform.Find("Canvas3D").transform as RectTransform;
            
            //场景UI需要和GameObject对应
            SceneLayer = canvas3D.Find("SceneLayer");
            
            MainLayer = canvas.Find("MainLayer");
            NormalLayer = canvas.Find("NormalLayer");
            InfoLayer = canvas.Find("InfoLayer");
            TipLayer = canvas.Find("TipLayer");
            TopLayer = canvas.Find("TopLayer");
        }

        //以Panel为单位，屏蔽点击事件
        public void Push(UIPanel panel)
        {
            /*if (panel == null) return;
            if (uiStack.Count > 0)
            {
                uiStack.Peek().OnPause();
            }

            uiStack.Push(panel);
            panel.OnResume();*/
        }

        public void Pop()
        {
            /*if (uiStack.Count > 0)
            {
                uiStack.Pop().OnPause();
                if(uiStack.Count > 0)
                    uiStack.Peek().OnResume();
            }*/
        }

        private GameObject LoadUIResource(UILayer type, string name, bool isCache = true)
        {
            Transform layer;
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            switch (type)
            {
                case UILayer.SceneLayer:
                    sb.Append("UIScene/");
                    layer = SceneLayer;
                    break;
                case UILayer.MainLayer:
                    sb.Append("UIMain/");
                    layer = MainLayer;
                    break;
                case UILayer.NormalLayer:
                    sb.Append("UINormal/");
                    layer = NormalLayer;
                    break;
                case UILayer.InfoLayer:
                    sb.Append("UIInfo/");
                    layer = InfoLayer;
                    break;
                case UILayer.TipLayer:
                    sb.Append("UITip/");
                    layer = TipLayer;
                    break;
                case UILayer.TopLayer:
                    sb.Append("UITop/");
                    layer = TopLayer;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            sb.Append(name);
            GameObject res = ResourceLoader.Load<GameObject>(ResourceType.UI, sb.ToString(), isCache);
            res.SetActive(false);
            UIPanel ui = res.GetComponent<UIPanel>();
            ui.layer = layer;
            
            return res;
        }

        //按照unity 先加载预制件再实例化的原则
        public UIPanel LoadUI(UILayer type, string name, bool isCache = true)
        {
            if (uiPaneDic.ContainsKey(name))
            {
                return uiPaneDic[name];
            }
            GameObject res = LoadUIResource(type, name, isCache);
            Transform layer = res.GetComponent<UIPanel>().layer;
            
            GameObject obj = GameObject.Instantiate(res, layer);
            UIPanel ui = obj.GetComponent<UIPanel>();
            uiPaneDic[name] = ui;
            return ui;
        }

        public UIPanel GetUIPanel(string name)
        {
            if (uiPaneDic.ContainsKey(name))
            {
                return uiPaneDic[name];
            }
            return null;
        }

        public void Clear()
        {
            foreach (var panel in uiPaneDic)
            {
                panel.Value.OnClose(true);
            }
            uiPaneDic.Clear();
        }
    }
}