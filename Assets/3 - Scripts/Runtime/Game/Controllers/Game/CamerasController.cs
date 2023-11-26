using Cinemachine;
using UnityEngine;

namespace Game
{
    public class CamerasController : BaseController
    {
        [SerializeField] private CinemachineVirtualCamera playerCamera;
        
        public void SetTarget(Transform root, Transform lookAt)
        {
            playerCamera.LookAt = lookAt;
            playerCamera.Follow = root;
        }
    }
}