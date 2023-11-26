using System.Collections.Generic;
using Leopotam.Ecs;

namespace Game.Ecs.Components
{
    public struct RelatedEntities : IEcsAutoReset<RelatedEntities>
    {
        private List<EcsEntity> entities;

        public readonly IReadOnlyList<EcsEntity> Entities => entities;

        public void Add(EcsEntity entity)
        {
            entities ??= new();

            if (!entities.Contains(entity))
            {
                entities.Add(entity);
            }
        }

        public void Remove(EcsEntity entity)
        {
            if (entities == null) return;

            entities.RemoveAll(x => !x.IsExist() || x.AreEquals(entity));
        }

        public void AutoReset(ref RelatedEntities c)
        {
            c.entities = null;
        }
    }
}