using Game.Ecs.Components;
using Leopotam.Ecs;

namespace Game.Ecs.Systems
{
    public class SpawnEnemySystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnemyTag> playerFilter;
        private readonly EcsFilter<SpawnEnemyTag> spawnFilter;

        private EcsWorld world;
        private GameState gameState;
        private LocationController locationController;
        private BattleInfo battleInfo => gameState.BattleInfo;
        
        public void Run()
        {
            
        }
    }
}