using Game.Ecs.Components;
using Leopotam.Ecs;

namespace Game.Ecs.Systems
{
    public class MoveUnitsSystem : IEcsRunSystem
    {
        private EcsFilter<TargetMoveData, UnitController, RenderTag>.Exclude<DestroyTag> targetFilter;
        private EcsFilter<InvokerMoveData, UnitController, RenderTag>.Exclude<DestroyTag> invokerFilter;
        private EcsFilter<Move, Skills>.Exclude<DestroyTag> moveFilter;

        private EcsWorld world;
        
        public void Run()
        {
            foreach (var i in moveFilter)
            {
                ref var skill = ref moveFilter.Get2(i);

                if (skill.CurrentSkill.IsExist())
                {
                    ref var invoker = ref moveFilter.GetEntity(i);
                    moveFilter.Get1(i).MoveAction?.Execute(invoker, skill.CurrentSkill, world);
                }
            }
            
            foreach (var i in targetFilter)
            {
                var target = targetFilter.Get1(i);
                var unitController = targetFilter.Get2(i);

                if (unitController.MoveController != null)
                {
                    unitController.MoveController.SetTarget(target.Position);
                }
            }

            foreach (var i in invokerFilter)
            {
                var invoker = invokerFilter.Get1(i);
                var unitController = invokerFilter.Get2(i);
                
                if (unitController.MoveController != null)
                {
                    unitController.MoveController.SetSpeed(invoker.Speed);
                }
            }
        }
    }
}