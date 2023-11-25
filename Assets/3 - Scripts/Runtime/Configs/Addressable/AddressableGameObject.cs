using System.Threading;
using Cysharp.Threading.Tasks;

namespace UnityEngine.AddressableAssets
{
    public class AddressableGameObject : BaseAddressableAsset<GameObject>
    {
        public AddressableGameObject(AssetReference asset) : base(asset)
        {
        }
        
        protected override async UniTask<GameObject> DoLoad(CancellationToken token)
        {
            handle = Addressables.InstantiateAsync(assetReference);
            asset = await handle.WithCancellation(token);
            return asset;
        }

        protected override async UniTask DoRelease()
        {
            Addressables.ReleaseInstance(handle);
            handle = default;
            asset = null;
        }
    }
}