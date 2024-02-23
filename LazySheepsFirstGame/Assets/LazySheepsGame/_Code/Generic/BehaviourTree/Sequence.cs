
namespace com.LazyGames.Dz.Ai
{
    public class Sequence : Node
    {
        public override NodeStates Evaluate()
        {
            bool anyChildRunning = false;
            foreach (var node in children)
            {
                switch (node.Evaluate())
                {
                    case NodeStates.Failure:
                        state = NodeStates.Failure;
                        return state;
                    case NodeStates.Success:
                        continue;
                    case NodeStates.Running:
                        anyChildRunning = true;
                        continue;
                    default:
                        state = NodeStates.Success;
                        return state;
                }
            }

            state = anyChildRunning ? NodeStates.Running : NodeStates.Success;
            return state;
        }
    }

}