using System.Collections.Generic;
using UnityEngine;

namespace CFramework.BT
{
    public abstract class CompositeNode : Node
    {
        public List<Node> children = new List<Node>();
        public override Node Clone()
        {
            CompositeNode node = Instantiate(this);
            node.children = children.ConvertAll(c=>c.Clone());
            return node;
        }

        public override void Activate(Database database)
        {
            base.Activate(database);
            foreach (Node child in children)
            {
                child.Activate(database);
            }
        }
    }
}