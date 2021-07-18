using UnityEngine;
using UnityEngine.Serialization;

namespace CFramework.BT
{
    public enum NodeState
    {
        Running,
        Success,
        Failure
    }

    public abstract class Node : ScriptableObject
    {
        [HideInInspector] public NodeState state = NodeState.Running;
        [HideInInspector] public bool isStarted = false;
        [HideInInspector] public string guid;
        [HideInInspector] public Vector2 position;
        protected Database _database;
        private bool activated=false;

        public NodeState Update()
        {
            if (!isStarted)
            {
                isStarted = true;
                OnStart();
            }

            state = OnUpdate();

            if (state == NodeState.Failure || state == NodeState.Success)
            {
                isStarted = false;
                OnStop();
            }

            return state;
        }
        
        public virtual void Activate (Database database) {
            if (activated) return ;
            this._database = database;
            activated = true;
        }

        public virtual Node Clone()
        {
            return Instantiate(this);
        }
        protected abstract void OnStart();
        protected abstract NodeState OnUpdate();
        protected abstract void OnStop();
    }
}