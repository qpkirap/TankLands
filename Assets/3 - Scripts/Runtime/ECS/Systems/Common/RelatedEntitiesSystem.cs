using Game.Ecs.Components;
using Leopotam.Ecs;

namespace Game.Ecs.Systems
{
    public class RelatedEntitiesSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RelatedEntities, DestroyTag> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                DestroyRelatedEntities(ref filter.Get1(i));
            }
        }

        private static void DestroyRelatedEntities(ref RelatedEntities relatedEntities)
        {
            if (relatedEntities.Entities == null) return;

            foreach (var entity in relatedEntities.Entities)
            {
                if (entity.IsExist())
                {
                    if (entity.Has<RelatedEntities>())
                    {
                        DestroyRelatedEntities(ref entity.Get<RelatedEntities>());
                    }

                    entity.Get<DestroyTag>();
                }
            }
        }
    }
}