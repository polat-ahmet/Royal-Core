using System;
using System.Threading.Tasks;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature.Builder
{
    public interface IFeatureFactory : IService
    {
        // Programatik ekleme (sync)
        T Add<T>(IFeature parent, string name, Func<string, IFeature, T> create, string id = null)
            where T : IFeature;

        // Programatik kaldırma
        void Remove(IFeature feature);

        // (İsteğe bağlı) Async versiyon (Addressables/indirme gibi durumlar için)
        Task<T> AddAsync<T>(IFeature parent, string name, Func<string, IFeature, T> create, string id = null)
            where T : IFeature;
    }
}