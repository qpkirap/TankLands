using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks.Linq;
using Game.Configs;
using Game.Ecs;
using Leopotam.Ecs;
using UnityEngine;

namespace Game
{
    public class GameController : MonoBehaviour
    {
        private readonly PreloaderManager preloaderManager = new();
        private GameState gameState = new();
        private GameSystems systems;
        private BattleController battleController;
        
        [SerializeField] private List<BaseController> controllers;
        [SerializeField] private ConfigProvider configProvider;
        
        private Dictionary<Type, object> injections;

        public EcsWorld World => systems.World;

        private async void Awake()
        {
            preloaderManager.Init(GetInjection(), transform);
            
            var collection = controllers.ToUniTaskAsyncEnumerable();
            await collection.ForEachAwaitAsync(async item => await (item.Init(GetInjection())));

            systems = new(injections: GetInjection());
            
            battleController = controllers.FirstOrDefault(x => x is BattleController) as BattleController;

            battleController.StartBattle();
        }

        private Dictionary<Type, object> GetInjection()
        {
            if (injections != null) return injections;
            
            injections = new Dictionary<Type, object>()
            {
                [typeof(PreloaderManager)] = preloaderManager,
                [typeof(GameState)] = gameState,
                [typeof(ConfigProvider)] = configProvider,
                [typeof(GameController)] = this
            };
            
            controllers.ForEach(controller => injections.Add(controller.GetType(), controller));

            return injections;
        }

        private void Update()
        {
            systems?.Tick();
        }
        
        public void RemoveAllEntities<TEntityType>() where TEntityType : struct =>
            systems.RemoveAllEntities<TEntityType>();
    }
}