using Leopotam.Ecs;

namespace Game.Ecs.Components
{
    public struct InputData : IEcsAutoReset<InputData>
    {
        public Game.InputState State { get; private set; }
        
        public void Set(Game.InputState state)
        {
            State = state;
        }

        public void AutoReset(ref InputData c)
        {
            c.State = null;
        }
    }
}