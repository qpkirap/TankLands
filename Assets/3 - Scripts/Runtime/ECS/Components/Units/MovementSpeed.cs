namespace Game.Ecs.Components
{
    public struct MovementSpeed
    {
        public float Speed { get; private set; }
        
        public MovementSpeed(float speed)
        {
            Speed = speed;
        }
        
        public static implicit operator float(MovementSpeed position) => position.Speed;
    }
}