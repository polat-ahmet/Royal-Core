using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services;
using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Ports;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Services
{
    public interface ITargetingService : IService
    {
        bool TryGetNearest(Vector2 origin, float range, out ITargetable best);
    }
}