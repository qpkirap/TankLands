using System.Linq;
using Game.Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Ecs.Systems
{
    public class SpawnPlayerUnitsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag> playerFilter;
        private readonly EcsFilter<SpawnPlayerTag> spawnFilter;

        private EcsWorld world;
        private GameState gameState;
        private LocationController locationController;
        private BattleInfo battleInfo => gameState.BattleInfo;
        
        public void Run()
        {
            foreach (var i in spawnFilter)
            {
                if (battleInfo != null && playerFilter.GetEntitiesCount() < battleInfo.PlayerDatas.Count())
                {
                    var delta = battleInfo.PlayerDatas.Count() - playerFilter.GetEntitiesCount();
                    var startIndex = Mathf.Clamp(playerFilter.GetEntitiesCount(), 0, battleInfo.PlayerDatas.Count() - 1);
                    var players = battleInfo.PlayerDatas.ToArray();
                    
                    for (int j = startIndex; j < delta; j++)
                    {
                        var playerData = players[startIndex + j];
                        
                        var entity = world.NewEntity();
                        
                        entity.Replace(new PlayerTag());
                        entity.Replace(new UnitData(playerData));
                        entity.Get<Position>().SetValue(locationController.GetPlayerSpawnPosition());
                    }
                }
            }
        }
    }
}