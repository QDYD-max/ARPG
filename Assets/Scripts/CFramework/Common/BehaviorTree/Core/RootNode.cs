

using UnityEngine;

namespace CFramework
{
    public class RootNode : Node
    {
        public Node child;
        protected override void OnStart()
        {
            
        }

        protected override NodeState OnUpdate()
        {
            return child.Update();
        }

        protected override void OnStop()
        {
        }

        public override Node Clone()
        {
            RootNode node = Instantiate(this);
            node.child = child.Clone();
            return node;
        }

        public override void Activate(GameObject go)
        {
            base.Activate(go);
            child.Activate(go);
        }
    }
}