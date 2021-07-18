namespace CFramework.BT
{
    public class RepeatNode : DecoratorNode
    {
        protected override void OnStart()
        {
            
        }

        protected override NodeState OnUpdate()
        {
            child.Update();
            return NodeState.Running;
        }

        protected override void OnStop()
        {
        }
    }
}