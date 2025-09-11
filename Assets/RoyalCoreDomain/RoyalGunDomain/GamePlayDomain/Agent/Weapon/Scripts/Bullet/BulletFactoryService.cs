using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services.ViewProvider;

namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Agent.Weapon.Scripts.Bullet
{
    public class BulletFactoryService : IBulletFactory
    {
        private readonly IViewProvider _views; // pool destekliyorsa şahane

        public BulletFactoryService(IViewProvider views)
        {
            _views = views;
        }

        public void SpawnBullet(in BulletSpawnInfo info)
        {
            var bullet = _views.LoadView<BulletView>(info.BulletKey);
            bullet.Fire(info);
        }
    }
}