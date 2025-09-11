using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context.Port;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Ports
{
    public interface IWeaponHolder : IPort
    {
        Transform WeaponMount { get; }
        float DamageMultiplier { get; }
    }
}