using System.Resources;
using UnityEngine;

namespace CFramework
{
    public enum CEventType
    {
        NumericChange,
        StateChange,
        RoleBattle,
    }

    public class RoleBattle
    {
        public GameObject Attacker;
        public GameObject DamagedEntity;
        public long value;
    }
    
    public class NumericChange
    {
        public GameObject Entity;
        public NumericType NumericType;
        public long Old;
        public long New;
    }

    public class StateChange
    {
        public GameObject Entity;
        public StateType StateType;
        public bool State;
    }
    
}