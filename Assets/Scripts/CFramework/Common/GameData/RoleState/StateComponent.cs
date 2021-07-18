using System;
using System.Collections.Generic;
using UnityEngine;

namespace CFramework
{
    public class StateComponent : MonoBehaviour
    {
        private Dictionary<StateType, bool> StateDic = new Dictionary<StateType, bool>();

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            foreach (StateType type in Enum.GetValues(typeof(StateType)))
            {
                StateDic[type] = false;
            }
        }

        public bool this[StateType stateType]
        {
            get => this.StateDic[stateType];
            set
            {
                bool v = this.StateDic[stateType];
                if (v == value)
                {
                    return;
                }

                StateDic[stateType] = value;

                OnUpdate(stateType);
            }
        }

        private void OnUpdate(StateType stateType)
        {
            bool State = StateDic[stateType];
            EventCenter.Instance.EventTrigger<StateChange>(CEventType.StateChange, new StateChange()
            {
                Entity = gameObject,
                StateType = stateType,
                State = State,
            });
        }
    }
}