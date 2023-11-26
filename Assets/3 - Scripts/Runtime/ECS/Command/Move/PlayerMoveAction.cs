using Game.Ecs.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Game.Ecs.Command.Move
{
    public class PlayerMoveAction : ActionCommand
    {
        private const float distance = 1;
        
        public override bool Execute(in EcsEntity invoker, in EcsEntity skill, EcsWorld world)
        {
            if (invoker.TryGet(out InputData data) 
                && invoker.TryGet(out UnitController unitController)
                && unitController.MoveController != null)
            {
                var currentPosition = unitController.MoveController.Position;
                var speed = invoker.Get<MovementSpeed>();
                var velocity = data.State.Move;

                if (velocity == Vector2.zero)
                {
                    invoker.Replace(new InvokerMoveData(currentPosition, speed));
                    invoker.Replace(new TargetMoveData(currentPosition));
                    
                    return true;
                }
                
                var currentRotation = unitController.MoveController.Rotation;
                
                //TODO задний ход не работает(агент не может двигаться задним ходом)
                
                var rotationMatrix = Matrix4x4.TRS(Vector3.zero, currentRotation, Vector3.one);
                var direction = rotationMatrix.MultiplyVector(new Vector3(velocity.x, 1, velocity.y)).normalized;
                
                var targetPosition = currentPosition + direction * distance;
                
                invoker.Replace(new InvokerMoveData(currentPosition, speed));
                invoker.Replace(new TargetMoveData(targetPosition));

                return true;
            }

            return true;
        }
    }
}