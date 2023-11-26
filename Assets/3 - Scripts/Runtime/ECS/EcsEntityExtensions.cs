using Game.Ecs.Components;
using Leopotam.Ecs;

namespace Game.Ecs
{
    public static class EcsEntityExtensions
    {
        public static bool IsExist(in this EcsEntity entity)
        {
            return !entity.IsNull() && entity.IsAlive();
        }
        
        public static bool TryGet<T>(in this EcsEntity entity, out T component)
            where T : struct
        {
            if (entity.IsExist() && entity.Has<T>())
            {
                component = entity.Get<T>();

                return true;
            }
            else
            {
                component = default;

                return false;
            }
        }
        
        public static bool TryHas<T>(in this EcsEntity entity)
            where T : struct
        {
            return entity.IsExist() && entity.Has<T>();
        }
        
        public static EcsEntity CreateEntity(this EcsWorld world, EntityType type = EntityType.Battle)
        {
            var entity = world.NewEntity();

            switch (type)
            {
                case EntityType.Battle: entity.Get<BattleEntityTag>(); break;
            }
            return entity;
        }
    }
    
    public enum EntityType
    {
        Battle
    }
}