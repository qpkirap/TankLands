using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using Game.Battle.Controllers;
using Game.Configs;
using UniRx;
using UnityEngine;

namespace Game
{
    public class PreloaderManager
    {
        private Transform root;
        private UnitsViewConfig unitsViewConfig;

        private readonly Dictionary<string, (IUnitController prefab, UnitViewData viewData)> prefabs = new();

        public Subject<string> OnUnLoadUnit { get; private set; } = new();
        
        public void Init(Dictionary<Type, object> injections, Transform root)
        {
            var configProvider = injections[typeof(ConfigProvider)] as ConfigProvider;
            unitsViewConfig = configProvider.GetConfig<UnitsViewConfig>();
            
            this.root = root;
        }

        public async UniTask PreloadBattleUnitsAsync(BattleInfo battleInfo)
        {
            if (battleInfo == null) return;

            var enemyIds = battleInfo.EnemyDatas.Select(x => x.ViewDataId).ToArray();
            var playerIds = battleInfo.PlayerDatas.Select(x => x.ViewDataId).ToArray();
            
            var mergeIds = enemyIds.Concat(playerIds).ToArray();

            var unitsIdsAsync = mergeIds.ToUniTaskAsyncEnumerable();

            await unitsIdsAsync.ForEachAwaitAsync(async item => await GetPrefabAsync(item));

            var excludeItems = prefabs.Keys.Except(mergeIds);

            await excludeItems.ToUniTaskAsyncEnumerable().ForEachAwaitAsync(async id =>
            {
                OnUnLoadUnit.OnNext(id);
                
                await UnloadItemAsync(id);
            });
        }
        
        public IUnitController GetPrefab(string id)
        {
            if (prefabs.TryGetValue(id, out var prefab))
            {
                return prefab.prefab;
            }
            
            return null;
        }

        public async UniTask<IUnitController> GetPrefabAsync(string id)
        {
            if (prefabs.TryGetValue(id, out var prefab)) return prefab.prefab;
            
            var item = unitsViewConfig.ViewDatas.FirstOrDefault(x => x.Id.Equals(id));
            
            if (item != null && item.Controller is { RuntimeKeyIsValid: true })
            {
                var go = await item.Controller.LoadAsync();

                if (go != null)
                {
                    var controller = go.GetComponent<IUnitController>();
                    prefabs.Add(id, (controller, item));
                    
                    controller.MonoBehaviour.gameObject.SetActive(false);
                    controller.MonoBehaviour.transform.SetParent(root, false);
                    
                    return controller;
                }
            }

            return null;
        }

        private async UniTask UnloadItemAsync(string id)
        {
            if (prefabs.TryGetValue(id, out var prefab))
            {
                await prefab.viewData.Controller.ReleaseAsync();
                
                prefabs.Remove(id);
            }
        }
    }
}