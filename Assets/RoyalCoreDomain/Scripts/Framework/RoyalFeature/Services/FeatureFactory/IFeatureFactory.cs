using System;
using System.Threading;
using System.Threading.Tasks;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services.FeatureFactory
{
    public interface IFeatureFactory : IService
    {
        // public void Build(IFeature root);
        
        // Programatik ekleme (sync)
        T Create<T>(IFeature parent, string name, Func<string, IFeature, T> create, string id = null)
            where T : IFeature;

        // Programatik kaldırma
        void Remove(IFeature feature);

        // (İsteğe bağlı) Async versiyon (Addressables/indirme gibi durumlar için)
        Task<T> CreateAsync<T>(IFeature parent, string name, Func<string, IFeature, T> create, CancellationTokenSource ct,
            string id = null)
            where T : IFeature;
    }
}