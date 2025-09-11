using System;
using System.Threading.Tasks;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature.Builder
{
    public sealed class FeatureFactory : IFeatureFactory
    {
        public T Add<T>(IFeature parent, string name, Func<string, IFeature, T> create, string id = null)
            where T : IFeature
        {
            if (create == null) throw new ArgumentNullException(nameof(create));
            var address = FeatureAddress.ChildOf(parent?.Address, name, id);
            var child = create(address, parent);
            child.Install();
            child.Start();
            return child;
        }

        public async Task<T> AddAsync<T>(IFeature parent, string name, Func<string, IFeature, T> create,
            string id = null)
            where T : IFeature
        {
            // Yer: Addressables/asset preload vs. – şu an direkt sync çağırıyoruz
            await Task.Yield();
            return Add(parent, name, create, id);
        }

        public void Remove(IFeature feature)
        {
            if (feature == null) return;
            // lifecycle simetri
            feature.Stop();
            feature.Dispose();
            feature.Parent?.RemoveChild(feature);
        }
    }
}