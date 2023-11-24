using UnityEngine;

namespace Game.Components
{
    public struct Position
    {
        private Vector3 value;

        public readonly Vector3 Value => value;

        public void SetValue(Vector3 value)
        {
            this.value = value;
        }

        public static implicit operator Position(Vector3 value) => new() { value = value };
        public static implicit operator Vector3(Position position) => position.value;
    }
}