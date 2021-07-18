using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace CFramework
{
    public static class Utils
    {
        public static T GetOrAddComponent<T>(this GameObject gameObject) where T : Component
        {
            if (gameObject.GetComponent<T>() == null)
            {
                gameObject.AddComponent<T>();
            }

            return gameObject.GetComponent<T>();
        }

        public static void AddCustomListener(this UIBehaviour control, EventTriggerType type,
            UnityAction<BaseEventData> callBack)
        {
            EventTrigger trigger =control.gameObject.GetOrAddComponent<EventTrigger>();
            EventTrigger.Entry entry = new EventTrigger.Entry();
            entry.eventID = type;
            entry.callback.AddListener(callBack);
            trigger.triggers.Add(entry);
        }
        
        public static string ColorToHex(Color32 color)
        {
            string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
            return hex;
        }
    }
}