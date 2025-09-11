namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Modules.Health
{
    public interface IHealthData
    {
        public float MaxHealth { get; set; }
        public float CurrentHealth { get; set; }
    }
}