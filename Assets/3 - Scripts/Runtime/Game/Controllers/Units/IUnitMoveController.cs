using Leopotam.Ecs;
using UnityEngine;

namespace Game.Battle.Controllers
{
    public interface IUnitMoveController
    {
        Quaternion Rotation { get; }
        Vector3 Position { get; }
        void InjectActivation(EcsEntity entity);
        void Disable();
        void SetTarget(Vector3? target);
        void SetSpeed(float speed);
    }
}
