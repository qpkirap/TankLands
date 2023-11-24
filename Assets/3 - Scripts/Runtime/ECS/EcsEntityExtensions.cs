using Leopotam.Ecs;

namespace Game.Components
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
    }
}