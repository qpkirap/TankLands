using Leopotam.Ecs;

namespace Game.Ecs.Command
{
    public abstract class Command
    {
        public virtual void Reset(in EcsEntity invoker, in EcsEntity skill, EcsWorld world)
        {
        }
    }
}