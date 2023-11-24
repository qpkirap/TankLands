using Leopotam.Ecs;
using UnityEngine;

namespace Game.Battle.Controllers
{
    public class UnitController : MonoBehaviour, IUnitController
    {
        private IUnitMoveController moveController;
        private EcsEntity entity;

        public IUnitMoveController UnitMoveController => moveController;
        
        private void Awake()
        {
            moveController = GetComponent<IUnitMoveController>();
        }

        public void InjectActivation(EcsEntity entity)
        {
            this.entity = entity;

            moveController?.InjectActivation(this.entity);
        }

        public void Disable()
        {
            moveController?.Disable();
        }
    }

    public interface IUnitController
    {
        IUnitMoveController UnitMoveController { get; }
        void InjectActivation(EcsEntity entity);
        void Disable();
    }
}