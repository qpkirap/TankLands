using UnityEngine;

namespace Game.Ecs.Components
{
    public struct InvokerMoveData
    {
        public float Speed { get; private set; }
        public Vector3 Position { get; private set; }
        
        public InvokerMoveData(Vector3 position, float speed)
        {
            Speed = speed;
            Position = position;
        }
    }
}