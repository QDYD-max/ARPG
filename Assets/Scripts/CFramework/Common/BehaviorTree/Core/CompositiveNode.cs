using System.Collections.Generic;
using UnityEngine;

namespace CFramework
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

        public override void Activate(GameObject go)
        {
            base.Activate(go);
            foreach (Node child in children)
            {
                child.Activate(go);
            }
        }
    }
}