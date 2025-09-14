using System;
using System.Threading.Tasks;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature.Builder
{
    public sealed class FeatureFactory : IFeatureFactory
    {
        // public void Build(IFeature root)
        // {
        //     root.PreInstall();
        //     root.Install();
        //     root.Resolve();
        //     root.Start();
        // }
        //
        public T AddDynamic<T>(IFeature parent, string name, Func<string, IFeature, T> create, string id = null)
            where T : IFeature
        {
            if (create == null) throw new ArgumentNullException(nameof(create));
            var address = FeatureAddress.ChildOf(parent?.Address, name, id);
            var child = create(address, parent);
            child.Build();
            Debug.Log("Feature Created");
            return child;
        }
        
        public async Task<T> AddAsync<T>(IFeature parent, string name, Func<string, IFeature, T> create,
            string id = null)
            where T : IFeature
        {
            Debug.Log("Feature Async Creating waiting");
            // Yer: Addressables/asset preload vs. – şu an direkt sync çağırıyoruz
            await Task.Delay(4000);
            Debug.Log("Feature Creating");
            return AddDynamic(parent, name, create, id);
        }

        public void Remove(IFeature feature)
        {
            if (feature == null) return;
            // lifecycle simetri
            feature.Stop();
            feature.Parent?.RemoveChild(feature);
            feature.Dispose();
        }
    
    }
}