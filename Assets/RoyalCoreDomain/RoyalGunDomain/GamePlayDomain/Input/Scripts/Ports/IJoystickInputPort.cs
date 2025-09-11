using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context.Port;
using UnityEngine;

namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Input.RoyalFeatureTemplate.Scripts.Ports
{
    public interface IJoystickInputPort : IPort
    {
        Vector2 InputVector { get; }
        bool IsActive { get; }
    }
}