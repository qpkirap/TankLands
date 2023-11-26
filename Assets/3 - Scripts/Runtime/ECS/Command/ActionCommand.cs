using Leopotam.Ecs;

namespace Game.Ecs.Command
{
    public abstract class ActionCommand : Command
    {
        public abstract bool Execute(in EcsEntity invoker, in EcsEntity skill, EcsWorld world);
    }
}