using System;
using System.Collections.Generic;
using Game.Ecs.Components;
using Leopotam.Ecs;

namespace Game.Ecs
{
    public class GameSystems : IDisposable
    {
        private readonly EcsWorld world;
        private readonly EcsSystems systems;
        
        public EcsWorld World => world;

        public GameSystems(Dictionary<Type, object> injections)
        {
            world = new EcsWorld();
            systems = new EcsSystems(world);
            
            CreateInjects(systems, injections);
            
            systems.Init();
        }
        
        public void Tick()
        {
            systems.Run();
        }
        
        public void RemoveAllEntities<TEntityType>() where TEntityType : struct
        {
            var filter = world.GetFilter(typeof(EcsFilter<TEntityType>));

            foreach (var i in filter)
            {
                filter.GetEntity(i).Get<Destroy>();
            }
        }
        
        private void CreateInjects(EcsSystems systems, Dictionary<Type, object> injections)
        {
            if (injections == null) return;

            foreach (var injection in injections)
            {
                systems.Inject(injection.Value, injection.Key);
            }

            systems.ProcessInjects();
        }

        public void Dispose()
        {
            systems.Destroy();
        }
    }
}