using System;
using UnityEngine;

namespace Game.Configs
{
    [Serializable]
    public class UnitData : ScriptableEntity
    {
        [SerializeField] private float health;
        [SerializeField] private float speed;
        [SerializeField] private float damage;
        [SerializeField] private float armor;
        [SerializeField][HideInInspector] private string viewDataKey;

        #region Editor

        public const string ViewDataKey = nameof(viewDataKey);
        public const string IndexViewKey = nameof(indexView);
        
        [SerializeField][HideInInspector] private int indexView;

        #endregion
        
        public float Health => health;
        public float Speed => speed;
        public float Damage => damage;
        public float Armor => armor;
        public string ViewDataId => viewDataKey;
    }
}