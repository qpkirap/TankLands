using Leopotam.Ecs;
using UnityEngine;

namespace Game.Battle.Controllers
{
    public class UnitController : MonoBehaviour, IUnitController
    {
        private IUnitMoveController moveController;
        private EcsEntity entity;

        public IUnitMoveController UnitMoveController => moveController;
        public MonoBehaviour MonoBehaviour => this;

        private void Awake()
        {
            moveController = GetComponent<IUnitMoveController>();
        }

        public virtual void InjectActivation(EcsEntity entity)
        {
            this.entity = entity;

            moveController?.InjectActivation(this.entity);
        }

        public virtual void Disable()
        {
            moveController?.Disable();
        }
    }

    public interface IUnitController
    {
        IUnitMoveController UnitMoveController { get; }
        MonoBehaviour MonoBehaviour { get; }
        void InjectActivation(EcsEntity entity);
        void Disable();
    }
}