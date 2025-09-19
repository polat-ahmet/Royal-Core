using System;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services;

namespace RoyalCoreDomain.Scripts.Services.Pool
{
    public interface IPoolRegistryService : IService
    {
        IPool<T> GetOrCreate<T>(PoolId id, Func<IPool<T>> create);
        bool TryGet<T>(PoolId id, out IPool<T> pool);
        void Remove(PoolId id);      // havuzu kapat/temizle
        void Clear();                // tüm havuzları kapat/temizle
    }
}