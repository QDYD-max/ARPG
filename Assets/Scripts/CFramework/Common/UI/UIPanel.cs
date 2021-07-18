using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace CFramework
{
    public abstract class UIPanel : MonoBehaviour
    {
        
        public Transform layer;
        public bool isPause = false;
        
        private Dictionary<string, List<UIBehaviour>> controlDic = new Dictionary<string, List<UIBehaviour>>();
        
        private void Awake()
        {
            FindChildrenControl<Button>();
            FindChildrenControl<Image>();
            FindChildrenControl<Text>();
            FindChildrenControl<Toggle>();
            FindChildrenControl<Slider>();
            FindChildrenControl<ScrollRect>();
            FindChildrenControl<InputField>();
            
        }

        //提供给外部初始化一些初始值
        public virtual void OnInit()
        {
            
        }

        public virtual void OnShow()
        {
            UIManager.Instance.Push(this);
            gameObject.SetActive(true);
        }

        public virtual void OnClose(bool isDestroy = false)
        {
            UIManager.Instance.Pop();
            gameObject.SetActive(false);
            if (isDestroy)
            {
                Destroy(this);
            }
        }
        
        public virtual void OnResume()
        {
            isPause = false;
            gameObject.GetOrAddComponent<CanvasGroup>().blocksRaycasts = true;
        }

        public virtual void OnPause()
        {
            isPause = true;
            gameObject.GetOrAddComponent<CanvasGroup>().blocksRaycasts = false;
        }
        

        //得到对应名字的对应控件脚本
        protected T GetControl<T>(string controlName) where T:UIBehaviour
        {
            if(controlDic.ContainsKey(controlName))
            {
                for(int i=0;i<controlDic[controlName].Count;++i)
                {
                    if (controlDic[controlName][i] is T)
                        return controlDic[controlName][i] as T;
                }
            }

            return null;
        }

        protected virtual void OnClick(string btnName)
        {
        }

        protected virtual void OnValueChanged(string toggleName, bool value)
        {
        }

        //找到子对象的对应控件
        private void FindChildrenControl<T>() where T:UIBehaviour
        {
            T[] controls = this.GetComponentsInChildren<T>();

            foreach (T control in controls)
            {
                string objName = control.gameObject.name;

                if (controlDic.ContainsKey(objName))
                    controlDic[objName].Add(control);
                else
                    controlDic.Add(control.gameObject.name,new List<UIBehaviour>() {control});

                switch (control)
                {
                    case Button button:
                        button.onClick.AddListener(() => 
                        {
                            OnClick(objName);
                        });
                        break;
                    case Toggle toggle:
                        toggle.onValueChanged.AddListener((value)=>
                        {
                            OnValueChanged(objName, value);
                        });
                        break;
                }
            }
        }
        
    }
}