using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    [Serializable][CreateAssetMenu]
    public class UnitsViewConfig : ScriptableEntity
    {
        [SerializeField][HideInInspector] private List<UnitViewData> viewDatas;
        
        public IReadOnlyList<UnitViewData> ViewDatas => viewDatas;

        #region Editor
        
        public const string ViewDatasKey = nameof(viewDatas);

        public void AddViewData(UnitViewData viewData)
        {
            viewDatas.Add(viewData);
        }

        public void RemoveViewData(int index)
        {
            if (index >= 0 && index < viewDatas.Count)
            {
                viewDatas.RemoveAt(index);
            }
        }
        
        public UnitViewData GetViewData(int index)
        {
            return index >= 0 && index < viewDatas.Count ? viewDatas[index] : null;
        }
        
        #endregion
    }
}