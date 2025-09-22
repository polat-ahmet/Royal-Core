using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context.Port;
using UnityEngine;

namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Agent.Weapon.Scripts.Ports
{
    public interface ITargetable : IPort
    {
        Transform Transform { get; } // pozisyon/rotasyon için
        bool IsAlive { get; } // ölüleri filtrele
    }
}