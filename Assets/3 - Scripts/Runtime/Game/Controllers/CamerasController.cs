using Cinemachine;
using UnityEngine;

namespace Game
{
    public class CamerasController : BaseController
    {
        [SerializeField] private CinemachineVirtualCamera playerCamera;
        
        public void SetTarget(Transform target)
        {
            playerCamera.LookAt = target;
            playerCamera.Follow = target;
        }
    }
}