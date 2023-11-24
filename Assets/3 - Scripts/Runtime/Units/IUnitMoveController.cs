using Leopotam.Ecs;
using UnityEngine;

namespace Game.Battle.Controllers
{
    public interface IUnitMoveController
    {
        void InjectActivation(EcsEntity entity);
        void Disable();
        void SetTarget(Vector3? target);
    }
}
