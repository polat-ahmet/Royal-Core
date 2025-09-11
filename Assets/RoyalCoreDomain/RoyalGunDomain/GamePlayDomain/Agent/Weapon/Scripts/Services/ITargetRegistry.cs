using System.Collections.Generic;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services;
using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Ports;

namespace RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Services
{
    public interface ITargetRegistry : IService
    {
        IReadOnlyCollection<ITargetable> All { get; }
        void Register(ITargetable t);
        void Unregister(ITargetable t);
    }
}