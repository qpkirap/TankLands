using UnityEngine;

namespace Game.Ecs.Components
{
    public struct TargetMoveData
    {
        public Vector3 Position { get; private set; }
        
        public TargetMoveData(Vector3 position)
        {
            Position = position;
        }
    }
}