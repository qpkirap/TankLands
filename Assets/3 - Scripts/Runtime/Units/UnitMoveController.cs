using Game.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Battle.Controllers
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class UnitMoveController : MonoBehaviour, IUnitMoveController
    {
        private const string walkZone = "Walkable";

        [SerializeField] private NavMeshAgent agent;

        private Transform transformCache;
        private int walkableLayerMask;
        private EcsEntity entity;
        private Vector3 targetMove;

        private void Awake()
        {
            transformCache = transform;
            
            var walkableArea = NavMesh.GetAreaFromName(walkZone);
            walkableLayerMask = 1 << walkableArea;
        }

        public virtual void InjectActivation(EcsEntity entity)
        {
            this.entity = entity;

            if (this.entity.TryGet(out Position position))
            {
                targetMove = position;
                
                if (agent != null) agent.Warp(GetNearestNavmesh(position));
            }

            SetMoveState(true);
        }

        public virtual void Disable()
        {
            SetMoveState(false);
        }

        public void SetMoveState(bool canMove)
        {
            if (agent != null && agent.isOnNavMesh)
            {
                agent.isStopped = !canMove;
            }

            if (!canMove)
            {
                targetMove = transformCache.position;
            }
        }

        public void SetTarget(Vector3? target)
        {
            if (targetMove == target) return;

            targetMove = target ?? transformCache.position;

            UpdateDestination();
        }

        public void SetSpeed(float speed)
        {
            agent.speed = speed;
        }

        private void UpdateDestination()
        {
            if (agent != null)
            {
                var nearest = GetNearestNavmesh(targetMove);
                
                if (nearest != default)
                {
                    agent.SetDestination(nearest);
                }
                else
                {
                    Debug.LogWarning("Цель находится в недоступном месте.");
                }
            }
        }

        private Vector3 GetNearestNavmesh(Vector3 position)
        {
            return NavMesh.SamplePosition(position, out var hit, 10.0f, walkableLayerMask) ? hit.position : default;
        }
    }
}