using UnityEngine;

namespace CFramework.BT
{
    public class DebugLogNode : ActionNode
    {
        public string message;

        protected override void OnStart()
        {
 
        }

        protected override NodeState OnUpdate()
        {
            Debug.Log(message);
            return NodeState.Success;
        }

        protected override void OnStop()
        {

        }
    }
}