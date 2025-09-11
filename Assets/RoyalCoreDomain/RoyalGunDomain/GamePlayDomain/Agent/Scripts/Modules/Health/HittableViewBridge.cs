namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Modules.Health
{
    public interface IHittableViewBridge : IHittable
    {
        public IHittable Target { get; }

        public void BindTarget(IHittable target);
    }
}