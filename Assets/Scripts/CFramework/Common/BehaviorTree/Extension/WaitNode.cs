
using TMPro.EditorUtilities;
using UnityEngine;

namespace CFramework
{
    public class WaitNode : ActionNode
    {
        public float duration;
        private float _srartTime;
        protected override void OnStart()
        {
            _srartTime = Time.time;
        }

        protected override NodeState OnUpdate()
        {
            return Time.time - _srartTime > duration ? NodeState.Success : NodeState.Running;
        }

        protected override void OnStop()
        {
            Debug.Log("等待结束");
        }
    }
}