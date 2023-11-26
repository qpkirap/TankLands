using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.Battle.Controllers;
using Game.Ecs;
using Game.Ecs.Components;
using Leopotam.Ecs;
using UniRx;
using UnityEngine.Pool;
using UnitController = Game.Ecs.Components.UnitController;
using UnitData = Game.Ecs.Components.UnitData;

namespace Game
{
    public class UnitsController : BaseController
    {
        private PreloaderManager preloaderManager;
        private readonly Dictionary<string, ObjectPool<IUnitController>> pools = new();

        private readonly CompositeDisposable disp = new();
        
        public override async UniTask Init(Dictionary<Type, object> injections)
        {
            if (disp.Count >0) disp.Clear();
            
            await base.Init(injections);
            
            injections.TryGetValue(typeof(PreloaderManager), out var preloaderManager);
            this.preloaderManager = preloaderManager as PreloaderManager;

            this.preloaderManager.OnUnLoadUnit.Subscribe(OnUnload).AddTo(disp);
        }

        public IUnitController CreateViewUnit(EcsEntity entity)
        {
            if (!entity.IsExist()) return null;

            if (entity.TryGet(out UnitData data))
            {
                var id = data.Data.ViewDataId;
                
                if (pools.TryGetValue(id, out var pool))
                {
                    return pool.Get();
                }

                if (entity.TryGet(out Position position)) ;
                
                var prefab = preloaderManager.GetPrefab(id);

                if (prefab == null) return null;
                
                var poolItem = new ObjectPool<IUnitController>(
                    () =>
                    {
                        var item = Instantiate(prefab.MonoBehaviour.gameObject, parent: transform);
                        
                        return item.GetComponent<IUnitController>();
                    }, 
                    controller =>
                    {
                        controller.MonoBehaviour.transform.position = position;
                        controller.MonoBehaviour.gameObject.SetActive(true);
                    },
                    controller => controller.MonoBehaviour.gameObject.SetActive(false),
                    controller => Destroy(controller.MonoBehaviour.gameObject));
                
                pools.Add(id, poolItem);
                
                return poolItem.Get();
            }

            return null;
        }

        public void ReleaseUnit(EcsEntity entity)
        {
            if (!entity.IsExist()) return;

            if (entity.TryGet(out UnitData unitData) && entity.TryGet(out UnitController unitController))
            {
                if (pools.TryGetValue(unitData.Data.Id, out var pool))
                {
                    unitController.Controller.Disable();
                    
                    pool.Release(unitController.Controller);
                }
            }
        }

        private void OnUnload(string id)
        {
            if (pools.TryGetValue(id, out var pool))
            {
                pool.Dispose();
            }
            
            pools.Remove(id);
        }

        private void OnDisable()
        {
            disp.Dispose();
        }
    }
}