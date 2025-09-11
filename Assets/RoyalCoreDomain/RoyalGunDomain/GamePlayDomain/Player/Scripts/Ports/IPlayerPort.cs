using RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Modules.Health;
using RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Modules.Movement;
using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Ports;
using UnityEngine;

namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Player.Scripts.Ports
{
    public interface IPlayerPort : IHittable, IMovable, IWeaponHolder
    {
        Transform transform { get; }
    }
}