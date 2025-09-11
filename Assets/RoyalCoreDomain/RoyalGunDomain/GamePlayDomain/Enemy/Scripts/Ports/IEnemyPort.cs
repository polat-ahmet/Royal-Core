using RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Modules.Health;
using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Ports;

namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Enemy.Scripts.Ports
{
    public interface IEnemyPort : IHittable, IWeaponHolder, ITargetable
    {
    }
}