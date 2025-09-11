namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Modules.Animation
{
    public interface IAnimatable
    {
        void PlayBool(string param, bool value);
        void PlayTrigger(string param);
    }
}