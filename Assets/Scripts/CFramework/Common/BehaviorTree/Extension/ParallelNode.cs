using System;
using UnityEngine;

namespace CFramework.BT
{
    public class ParallelNode : CompositeNode
    {
        public ParallelType Type;
        protected override void OnStart()
        {
            
        }

        protected override NodeState OnUpdate()
        {
            switch (Type)
            {
                case ParallelType.And:
                    int count = 0;
                    foreach (var child in children)
                    {
                        switch (child.Update())
                        {
                            case NodeState.Failure:
                                return NodeState.Failure;
                            case NodeState.Running:
                                return NodeState.Running;
                            case NodeState.Success:
                                ++count;
                                break;
                        }
                    }

                    if (count == children.Count)
                        return NodeState.Success;
                    break;
                case ParallelType.Or:
                    foreach (var child in children)
                    {
                        switch (child.Update())
                        {
                            case NodeState.Failure:
                                return NodeState.Failure;
                            case NodeState.Running:
                                return NodeState.Running;
                            case NodeState.Success:
                                return NodeState.Success;
                        }
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            

            return NodeState.Success;
        }

        protected override void OnStop()
        {
            
        }

        public enum ParallelType
        {
            And,
            Or
        }
    }
}