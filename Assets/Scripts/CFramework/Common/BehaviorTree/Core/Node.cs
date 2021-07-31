using UnityEngine;
using UnityEngine.Serialization;

namespace CFramework
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
        protected GameObject entity;
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
        
        public virtual void Activate (GameObject go) {
            if (activated) return ;
            this.entity = go;
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