using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Configs
{
    [Serializable]
    public class UnitViewData : ScriptableEntity
    {
        [SerializeField] private AssetReference controller;

        public AddressableGameObject Controller => new(controller);
    }
}