using Game.Ecs.Components;
using Leopotam.Ecs;

namespace Game.Ecs.Systems
{
    public class DestroySystem : IEcsRunSystem
    {
        private readonly EcsFilter<DestroyTag> destroyFilter;

        public void Run()
        {
            foreach (var i in destroyFilter)
            {
                var entity = destroyFilter.GetEntity(i);
                if (entity.IsExist()) entity.Destroy();
            }
        }
    }
}