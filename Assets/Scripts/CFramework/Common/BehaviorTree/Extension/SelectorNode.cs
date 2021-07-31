namespace CFramework
{
    public class SelectorNode : CompositeNode
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
                case NodeState.Failure:
                    ++_index;
                    break;
                //case NodeState.Running:
                //    return NodeState.Running;
                case NodeState.Success:
                    return NodeState.Success;
            }
            
            return children.Count == _index ? NodeState.Failure : NodeState.Running;
        }

        protected override void OnStop()
        {
            
        }
    }
}