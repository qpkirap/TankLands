using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Configs;
using Game.Configs.Data;
using Game.Ecs;
using Game.Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class BattleController : BaseController
    {
        private GameState gameState;
        
        private PreloaderManager preloaderManager;
        private ConfigProvider configProvider;
        
        private MapsConfig mapsConfig;
        private UnitsConfig unitsConfig;
        
        private UIController uiController;
        private LocationController locationController;
        private GameController gameController;
        
        public override async UniTask Init(Dictionary<Type, object> injections)
        {
            await base.Init(injections);
            
            injections.TryGetValue(typeof(ConfigProvider), out var configProvider);
            this.configProvider = configProvider as ConfigProvider;
            
            mapsConfig = this.configProvider.GetConfig<MapsConfig>();
            unitsConfig = this.configProvider.GetConfig<UnitsConfig>();
            
            injections.TryGetValue(typeof(PreloaderManager), out var preloaderManager);
            this.preloaderManager = preloaderManager as PreloaderManager;
            
            injections.TryGetValue(typeof(GameState), out var gameState);
            this.gameState = gameState as GameState;
            
            injections.TryGetValue(typeof(UIController), out var uiController);
            this.uiController = uiController as UIController;
            
            injections.TryGetValue(typeof(LocationController), out var locationController);
            this.locationController = locationController as LocationController;
            
            injections.TryGetValue(typeof(GameController), out var gameController);
            this.gameController = gameController as GameController;
        }

        public async UniTask StartBattle(int locationId = 0)
        {
            uiController.SetLoader(true);
            
            var item = mapsConfig.MapDatas.FirstOrDefault(x => x.Id.Equals(locationId));

            if (item != null)
            {
                var enemyDatas = unitsConfig.UnitDatas.Where(x => item.Enemys.Contains(x.Id));
                var playerDatas = unitsConfig.UnitDatas.Where(x => item.Player.Equals(x.Id));
                
                var battleInfo = gameState.CreateBattle(playerDatas, enemyDatas, locationId, item.MaxEnemyCount);
                
                var preload = preloaderManager.PreloadBattleUnitsAsync(battleInfo);
                var map = locationController.CreateMap(locationId);
                
                await UniTask.WhenAll(preload, map);
                
                uiController.SetLoader(false);

                var startBattle = gameController.World.CreateEntity();
                startBattle.Get<SpawnPlayerTag>();
                startBattle.Get<SpawnEnemyTag>();
                startBattle.Get<DestroyTag>();
            }
            else Debug.LogError("Map not found");
        }

        public async UniTask EndBattle()
        {
            gameController.RemoveAllEntities<BattleEntityTag>();

            await UniTask.DelayFrame(5);
        }
    }
}