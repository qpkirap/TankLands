using UnityEngine;

namespace Game
{
    public class UIController : BaseController
    {
        [SerializeField] private GameObject loader;
        
        public void SetLoader(bool isActive)
        {
            loader.gameObject.SetActive(isActive);
        }
    }
}