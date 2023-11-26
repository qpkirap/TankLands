using Game.Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Battle.Controllers
{
    public class PlayerUnitController : UnitController
    {
        [SerializeField] private Transform cameraRoot;
        [SerializeField] private Transform cameraLookAt;

        public override void InjectActivation(EcsEntity entity)
        {
            base.InjectActivation(entity);
            
            entity.Get<CameraFollow>().Init(cameraRoot, cameraLookAt);
        }
    }
}