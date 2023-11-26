using Game.Ecs.Components;
using Leopotam.Ecs;

namespace Game.Ecs.Systems
{
    public class CameraFollowViewSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraFollow>.Exclude<DestroyTag> filter;
        
        private CamerasController camerasController;
        
        public void Run()
        {
            foreach (var i in filter)
            {
                var cFollow = filter.Get1(i);

                if (cFollow.LookAt != null && cFollow.Root != null)
                {
                    camerasController.SetTarget(cFollow.Root, cFollow.LookAt);
                }
                
                filter.GetEntity(i).Del<CameraFollow>();
            }
        }
    }
}