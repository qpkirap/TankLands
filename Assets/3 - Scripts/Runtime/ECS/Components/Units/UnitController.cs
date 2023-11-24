using Game.Battle.Controllers;
using Leopotam.Ecs;

namespace Game.Components.Units
{
    public struct UnitController : IEcsAutoReset<UnitController>
    {
        public IUnitController Controller { get; private set; }
        public IUnitMoveController MoveController => Controller?.UnitMoveController;
        
        public void Init(EcsEntity entity, IUnitController view)
        {
            Controller = view;
        }

        public void AutoReset(ref UnitController c)
        {
            c.Controller = null;
        }
    }
}