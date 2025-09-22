using System.Threading;
using System.Threading.Tasks;
using RoyalCoreDomain.Scripts.Services.SceneLoader;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature
{
    public abstract class SceneOwnerFeature : BaseFeature
    {
        protected abstract string SceneKey { get; }

        private ISceneLoaderService _scenes;
        private CancellationTokenSource _ct;

        protected SceneOwnerFeature(string address, IFeature parent) : base(address, parent) { }

        protected override void OnResolve()
        {
            _scenes = Context.ImportService<ISceneLoaderService>();
        }

        protected override async Task OnWarmupAsync(CancellationTokenSource ct)
        {
            _ct = ct;
            
            await _scenes.TryLoadScene(SceneKey, ct);

            // Sahneye ihtiyaç duyan çocukların kurulumu:
            await OnSceneReadyAsync(ct);
        }

        /// Parent sahnesi yüklendi → sahneye bağlı çocukları burada async kur.
        protected virtual Task OnSceneReadyAsync(CancellationTokenSource ct) => Task.CompletedTask;

        protected override void OnDispose()
        {
            // Çocuklar factory tarafından ters sırayla dispose edilecek.
            // En sonda sahneyi kapat:
         
            // fire-and-forget (isteğe bağlı ct kullanılabilir)
            _ = _scenes.TryUnloadScene(SceneKey, default);
        }
    }
}