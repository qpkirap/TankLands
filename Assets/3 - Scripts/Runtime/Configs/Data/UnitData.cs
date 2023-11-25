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
        [SerializeField] private string viewDataKey;

        public const string ViewDataKey = nameof(viewDataKey);
    }
}