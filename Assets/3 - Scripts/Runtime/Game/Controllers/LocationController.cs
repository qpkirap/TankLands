using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Game.Configs;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Game
{
    public class LocationController : BaseController
    {
        [SerializeField] private Transform rootMaps;

        private MapViewConfig mapViewConfig;
        private (int id, MapContainer container, MapViewData mapData) currentMap;

        public override async UniTask Init(Dictionary<Type, object> injections)
        {
             await base.Init(injections);
             
             var configProvider = injections[typeof(ConfigProvider)] as ConfigProvider;
             mapViewConfig = configProvider.GetConfig<MapViewConfig>();
        }
        
        public Vector3 GetPlayerSpawnPosition()
        {
            if (currentMap != default)
            {
                var playerPoints = currentMap.container.PlayerPoint;
                
                return playerPoints.position;
            }
            
            return Vector3.zero;
        }

        public Vector3 GetEnemySpawnPosition()
        {
            if (currentMap != default)
            {
                var enemyPoints = currentMap.container.EnemyPoints[Random.Range(0, currentMap.container.EnemyPoints.Count)];
                
                return enemyPoints.position;
            }
            
            return Vector3.zero;
        }

        public async UniTask<MapContainer> CreateMap(int id)
        {
            if (currentMap.id == id && currentMap.container != null)
            {
                return currentMap.container;
            }
            if(currentMap.mapData != null)
            {
                await currentMap.mapData.Go.ReleaseAsync();
            }

            var item = mapViewConfig.Maps?.FirstOrDefault(x => x.Id == id);

            if (item is { Go: { RuntimeKeyIsValid: true } })
            {
                var map = await item.Go.LoadAsync();

                if (map != null)
                {
                    var container = map.GetComponent<MapContainer>();
                    
                    container.transform.SetParent(rootMaps, false);
                    
                    currentMap = (id, container, item);
                    
                    NavMesh.AddNavMeshData(item.NavMeshData);
                    
                    return container;
                }
            }

            return null;
        }
    }
}