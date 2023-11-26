using System;
using System.Collections.Generic;
using Game.Ecs.Components;
using Game.Ecs.Systems;
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

            systems
                .Add(new SpawnPlayerUnitsSystem())
                .Add(new SpawnUnitsViewSystem())
                .Add(new CameraFollowViewSystem())
                .Add(new MoveUnitsSystem())

                .Add(new DestroyUnitViewSystem())
                .Add(new RelatedEntitiesSystem())
                .Add(new DestroySystem());
            
            AddOneFrameComponents(systems);
            
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
                filter.GetEntity(i).Get<DestroyTag>();
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
        
        private void AddOneFrameComponents(EcsSystems systems)
        {
            systems
                .OneFrame<SpawnPlayerTag>()
                .OneFrame<SpawnEnemyTag>();
        }

        public void Dispose()
        {
            systems.Destroy();
        }
    }
}