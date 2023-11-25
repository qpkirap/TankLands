using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Unity.Plastic.Newtonsoft.Json;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UnityEngine.AddressableAssets
{
    public abstract class BaseAddressableAsset<TAsset> : IDisposable
    {
        private const string invalidHandleName = "InvalidHandle";

        [SerializeField, JsonProperty] protected AssetReference assetReference = new();

        protected TAsset asset;
        protected AsyncOperationHandle<TAsset> handle;

        protected bool Disposed { get; private set; }
        protected bool Released { get; private set; }

        public string AssetGUID => assetReference.AssetGUID;
        public bool RuntimeKeyIsValid => assetReference.RuntimeKeyIsValid();

        public BaseAddressableAsset()
        {
        }

        public BaseAddressableAsset(AssetReference asset)
        {
            assetReference = new AssetReference(asset.AssetGUID)
            {
                SubObjectName = asset.SubObjectName
            };
        }

        public async UniTask<TAsset> LoadAsync(CancellationToken token = default)
        {
            if (AssetExist())
            {
                return asset;
            }

            if (!RuntimeKeyIsValid)
            {
                return default;
            }

            Disposed = false;
            Released = false;

            return await DoLoad(token);
        }

        internal virtual bool AssetExist()
        {
            return asset != null;
        }

        public async void Dispose()
        {
            if (Disposed) return;

            Disposed = true;

            await ReleaseAsync();
        }

        public async void Release()
        {
            await ReleaseAsync();
        }

        public async UniTask ReleaseAsync()
        {
            if (Released) return;

            Released = true;

            if (asset != null || handle.DebugName != invalidHandleName)
            {
                await DoRelease();
            }
        }

        protected virtual async UniTask<TAsset> DoLoad(CancellationToken token = default)
        {
            handle = Addressables.LoadAssetAsync<TAsset>(assetReference);
            await handle.WithCancellation(token);

            asset = handle.Result;
            
            return asset;
        }

        protected virtual async UniTask DoRelease()
        {
            Addressables.Release(handle);
            handle = default;
            asset = default;
        }

        public static implicit operator AssetReference(BaseAddressableAsset<TAsset> asset) => asset.assetReference;
    }
}