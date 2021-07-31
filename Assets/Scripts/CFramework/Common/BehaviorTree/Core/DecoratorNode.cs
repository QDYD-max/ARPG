using UnityEngine;

namespace CFramework
{
    public abstract class DecoratorNode : Node
    {
        public Node child;
        
        public override Node Clone()
        {
            DecoratorNode node = Instantiate(this);
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