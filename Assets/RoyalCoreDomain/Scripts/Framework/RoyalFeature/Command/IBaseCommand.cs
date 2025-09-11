using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Command
{
    public interface IBaseCommand
    {
        void SetContext(FeatureContext context);
        void ResolveDependencies();
    }
}