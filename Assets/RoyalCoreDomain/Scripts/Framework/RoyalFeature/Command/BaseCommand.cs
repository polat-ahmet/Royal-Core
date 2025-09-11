using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Command
{
    public abstract class BaseCommand : IBaseCommand
    {
        protected FeatureContext _context;

        public void SetContext(FeatureContext context)
        {
            _context = context;
        }

        public abstract void ResolveDependencies();
    }
}