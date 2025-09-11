using System.Collections.Generic;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature
{
    public interface IFeatureRegistry : IService
    {
        void Register(IFeature f);
        void Unregister(string address);
        bool TryGet(string address, out IFeature f);
        IEnumerable<IFeature> All();
    }
}