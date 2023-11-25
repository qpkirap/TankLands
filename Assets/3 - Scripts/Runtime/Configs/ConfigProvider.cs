using System.Collections.Generic;
using UnityEngine;

namespace Game.Configs
{
    public class ConfigProvider : MonoBehaviour
    {
        [SerializeField] private List<ScriptableEntity> configs;
        
        public T GetConfig<T>() where T : ScriptableEntity
        {
            foreach (var config in configs)
            {
                if (config is T t)
                    return t;
            }
            return default;
        }
    }
}