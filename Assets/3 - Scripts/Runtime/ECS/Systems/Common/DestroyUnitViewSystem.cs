using Game.Ecs.Components;
using Leopotam.Ecs;

namespace Game.Ecs.Systems
{
    public class DestroyUnitViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnitController, DestroyTag> filter;

        private UnitsController viewController;

        public void Run()
        {
            foreach (var i in filter)
            {
                var entity = filter.GetEntity(i);
                
                viewController.ReleaseUnit(entity);

                entity.Del<UnitController>();
            }
        }
    }
}