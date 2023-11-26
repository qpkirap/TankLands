using Game.Ecs.Components;
using Leopotam.Ecs;

namespace Game.Ecs.Systems
{
    public class SkillExecuteSystem : IEcsRunSystem
    {
        private EcsFilter<Skill, ExecutionTag> skillFilter;
        
        public void Run()
        {
            throw new System.NotImplementedException();
        }
    }
}