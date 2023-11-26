using UnityEngine;

namespace Game
{
    public class InputState
    {
        public Vector2 Move { get; private set; }
        public Vector2 Target { get; private set; }
        public bool Fire { get; private set; }
        public bool NextSkill { get; private set; }
        public bool PreviousSkill { get; private set; }

        public void MoveAction(Vector2 move, bool isPress)
        {
            if (isPress) Move += move;
            else Move -= move;
            
            Debug.Log(Move);
        }
    }
}