using System.Collections.Generic;
using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Ports;

namespace RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Services
{
    public sealed class TargetRegistry : ITargetRegistry
    {
        private readonly HashSet<ITargetable> _set = new();
        public IReadOnlyCollection<ITargetable> All => _set;

        public void Register(ITargetable t)
        {
            if (t != null) _set.Add(t);
        }

        public void Unregister(ITargetable t)
        {
            if (t != null) _set.Remove(t);
        }
    }
}