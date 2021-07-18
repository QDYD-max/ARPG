using UnityEngine;

namespace CFramework.BT
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

        public override void Activate(Database database)
        {
            base.Activate(database);
            child.Activate(database);
        }
    }
}