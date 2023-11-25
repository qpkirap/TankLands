using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs.Data
{
    [Serializable][CreateAssetMenu]
    public class UnitsConfig : ScriptableEntity
    {
        [SerializeField][HideInInspector] private List<UnitData> unitDatas;

        
        public IReadOnlyList<UnitData> UnitDatas => unitDatas;

        #region Editor

        public const string UnitDatasKey = nameof(unitDatas);

        public void Add(UnitData unitData)
        {
            unitDatas.Add(unitData);
        }
        
        public void RemoveViewData(int index)
        {
            if (index >= 0 && index < unitDatas.Count)
            {
                unitDatas.RemoveAt(index);
            }
        }
        
        public UnitData GetViewData(int index)
        {
            return index >= 0 && index < unitDatas.Count ? unitDatas[index] : null;
        }

        #endregion
    }
}