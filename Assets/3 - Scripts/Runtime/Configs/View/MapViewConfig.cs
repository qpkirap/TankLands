using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AI;

namespace Game.Configs
{
    [Serializable][CreateAssetMenu]
    public class MapViewConfig : ScriptableEntity
    {
        [SerializeField] private List<MapViewData> maps;
        
        public IReadOnlyList<MapViewData> Maps => maps;
    }

    [Serializable]
    public class MapViewData
    {
        [field: SerializeField] private int id;
        [field: SerializeField] private AssetReference mapContainer;
        [field: SerializeField] private NavMeshData navMeshData;
        
        public int Id => id;
        public AddressableGameObject Go => new(mapContainer);
        
        public NavMeshData NavMeshData => navMeshData;
    }
}