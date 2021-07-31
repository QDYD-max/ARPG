using System;
using UnityEngine;

namespace CFramework
{
    public class ParallelNode : CompositeNode
    {
        public ParallelType parallelType;
        protected override void OnStart()
        {
            
        }

        protected override NodeState OnUpdate()
        {
            switch (parallelType)
            {
                case ParallelType.And:
                    int count = 0;
                    foreach (var child in children)
                    {
                        switch (child.Update())
                        {
                            case NodeState.Failure:
                            case NodeState.Success:
                                ++count;
                                break;
                        }
                    }
                    
                    return count == children.Count?NodeState.Success:NodeState.Running;
                    
                case ParallelType.Or:
                    foreach (var child in children)
                    {
                        switch (child.Update())
                        {
                            case NodeState.Failure:
                                return NodeState.Failure;
                            case NodeState.Success:
                                return NodeState.Success;
                        }
                    }
                    return NodeState.Running;

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