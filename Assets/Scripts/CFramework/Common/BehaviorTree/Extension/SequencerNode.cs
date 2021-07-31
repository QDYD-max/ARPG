using System;
using UnityEngine;

namespace CFramework
{
    public class SequencerNode : CompositeNode
    {
        private int _index;
        protected override void OnStart()
        {
            Debug.Log("开始此轮");
            _index = 0;
        }

        protected override NodeState OnUpdate()
        {
            switch (children[_index].Update())
            {
                case NodeState.Failure:
                    return NodeState.Failure;
                //case NodeState.Running:
                //    return NodeState.Running;
                case NodeState.Success:
                    ++_index;
                    break;
            }

            return children.Count == _index ? NodeState.Success : NodeState.Running;
        }

        protected override void OnStop()
        {
            
        }
    }
}