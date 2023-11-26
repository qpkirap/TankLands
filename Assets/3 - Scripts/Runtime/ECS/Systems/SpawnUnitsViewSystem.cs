using Game.Ecs.Components;
using Leopotam.Ecs;

namespace Game.Ecs
{
    public class SpawnUnitsViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnitData>.Exclude<RenderTag, DestroyTag> filter;

        private UnitsController unitsController;
        
        public void Run()
        {
            foreach (var i in filter)
            {
                var entity = filter.GetEntity(i);
                
                var controller = unitsController.CreateViewUnit(entity);

                if (controller != null)
                {
                    entity.Get<RenderTag>();

                    entity.Get<UnitController>().Init(controller);
                    
                    controller.InjectActivation(entity);
                }
                else entity.Get<DestroyTag>();
            }
        }
    }
}