using Leopotam.Ecs;
using UnityEngine;

namespace Game.Components
{
    public struct TargetMove : IEcsAutoReset<TargetMove>
    {
        public Vector3 Target { get; private set; }

        TargetMove(Vector3 target)
        {
            this.Target = target;
        }

        public void AutoReset(ref TargetMove c)
        {
            c.Target = default;
        }
    }
}