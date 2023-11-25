using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    [CreateAssetMenu][Serializable]
    public class MapsConfig : ScriptableEntity
    {
        [SerializeField] private List<MapData> mapDatas;
        
        public IReadOnlyList<MapData> MapDatas => mapDatas;
    }

    [Serializable]
    public class MapData
    {
        [field: SerializeField] private int id;
        [field: SerializeField] private List<string> enemys;
        [field: SerializeField] private string player;
        
        public int Id => id;
        public IReadOnlyList<string> Enemys => enemys;
        public string Player => player;
    }
}