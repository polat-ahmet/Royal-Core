using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context.Port;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Ports
{
    public interface IWeaponPort : IPort
    {
        bool IsFiring { get; }
        bool IsReloading { get; }
        int AmmoInMag { get; }
        int MagSize { get; }
        bool CanFire { get; }

        void SetAim(Vector2 worldDir);
        void FireStart();
        void FireStop();
        void Reload();
    }
}