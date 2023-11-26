using Game.Ecs.Command;

namespace Game.Ecs.Components
{
    public struct Move
    {
        public ActionCommand MoveAction { get; private set; }
        
        public Move(ActionCommand moveAction)
        {
            MoveAction = moveAction;
        }
    }
}