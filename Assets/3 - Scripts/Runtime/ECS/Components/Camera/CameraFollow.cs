using Leopotam.Ecs;
using UnityEngine;

namespace Game.Ecs.Components
{
    public struct CameraFollow : IEcsAutoReset<CameraFollow>
    {
        public Transform Root { get; private set; }
        public Transform LookAt { get; private set; }
        
        public void Init(Transform root, Transform lookAt)
        {
            this.Root = root;
            this.LookAt = lookAt;
        }

        public void AutoReset(ref CameraFollow c)
        {
            c.Root = default;
            c.LookAt = default;
        }
    }
}