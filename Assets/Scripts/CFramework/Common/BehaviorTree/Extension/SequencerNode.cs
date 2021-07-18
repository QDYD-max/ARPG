using System;
using UnityEngine;

namespace CFramework.BT
{
    public class SequencerNode : CompositeNode
    {
        private int _index;
        protected override void OnStart()
        {
            _index = 0;
        }

        protected override NodeState OnUpdate()
        {
            switch (children[_index].Update())
            {
                case NodeState.Running:
                    return NodeState.Running;
                case NodeState.Success:
                    ++_index;
                    break;
                case NodeState.Failure:
                    return NodeState.Failure;
            }

            return children.Count == _index ? NodeState.Success : NodeState.Running;
        }

        protected override void OnStop()
        {
            
        }
    }
}