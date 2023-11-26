using Leopotam.Ecs;

namespace Game.Ecs.Components
{
    public struct Skills
    {
        public EcsEntity CurrentSkill { get; private set; }
        
        public Skills(EcsEntity currentSkill)
        {
            CurrentSkill = currentSkill;
        }
    }
}