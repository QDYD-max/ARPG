using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CFramework
{
    public interface IEventInfo
    {
    }

    public class EventInfo<T> : IEventInfo
    {
        public UnityAction<T> actions;

        public EventInfo(UnityAction<T> action)
        {
            actions += action;
        }
    }

    public class EventInfo : IEventInfo
    {
        public UnityAction actions;

        public EventInfo(UnityAction action)
        {
            actions += action;
        }
    }

    public class EventCenter : Singleton<EventCenter>
    {
        //key事件的名称
        //value 对应的是监听这个事件的委托函数
        private Dictionary<string, IEventInfo> _eventDic = new Dictionary<string, IEventInfo>(); 
        //如果在这里用泛型那么EventCenter也要用泛型，但是EventCenter是一个单例，只有一个。
        //所以这里用了一个空接口去代替EventInfo<T>,之后用里氏转换原则，用基类代替子类

        //添加事件监听
        //这里如果只传UnityAction，当有多个相同对象时，不知道是具体哪一个，所以要用装箱UnityAction<object>
        public void AddEventListener<T>(string name, UnityAction<T> action) 
        {
            if (_eventDic.ContainsKey(name))
            {
                (_eventDic[name] as EventInfo<T>).actions += action; //用里氏转换原则，用基类代替子类
            }
            else
            {
                _eventDic.Add(name, new EventInfo<T>(action));
            }
        }

        public void AddEventListener(string name, UnityAction action)
        {
            if (_eventDic.ContainsKey(name))
            {
                // ReSharper disable once PossibleNullReferenceException
                (_eventDic[name] as EventInfo).actions += action; //用里氏转换原则，用基类代替子类
            }
            else
            {
                _eventDic.Add(name, new EventInfo(action));
            }
        }
        
        public void AddEventListener<T>(CEventType type, UnityAction<T> action)
        {
            AddEventListener<T>(type.ToString(), action);
        }

        public void AddEventListener(CEventType type, UnityAction action)
        {
            AddEventListener(type.ToString(), action);
        }

        //移除对应的事件监听
        public void RemoveEventListener<T>(string name, UnityAction<T> action)
        {
            if (_eventDic.ContainsKey(name))
                (_eventDic[name] as EventInfo<T>).actions -= action;
        }

        //移除对应的事件监听(不需要参数)
        public void RemoveEventListener(string name, UnityAction action)
        {
            if (_eventDic.ContainsKey(name))
                (_eventDic[name] as EventInfo).actions -= action;
        }
        
        public void RemoveEventListener<T>(CEventType type, UnityAction<T> action)
        {
            RemoveEventListener<T>(type.ToString(), action);
        }
        
        public void RemoveEventListener(CEventType type, UnityAction action)
        {
            RemoveEventListener(type.ToString(), action);
        }

        //事件触发
        public void EventTrigger<T>(string name, T info)
        {
            if (_eventDic.ContainsKey(name))
            {
                if ((_eventDic[name] as EventInfo<T>).actions != null)
                    (_eventDic[name] as EventInfo<T>).actions.Invoke(info);
            }
        }

        //事件触发(不需要参数)
        public void EventTrigger(string name)
        {
            if (_eventDic.ContainsKey(name))
            {
                if ((_eventDic[name] as EventInfo).actions != null)
                    (_eventDic[name] as EventInfo).actions.Invoke();
            }
        }
        
        public void EventTrigger<T>(CEventType type, T info)
        {
            EventTrigger<T>(type.ToString(), info);
        }
        
        public void EventTrigger(CEventType type)
        {
            EventTrigger(type.ToString());
        }

        //清空事件中心
        //主要用在场景切换
        public void Clear()
        {
            _eventDic.Clear();
        }
    }
}