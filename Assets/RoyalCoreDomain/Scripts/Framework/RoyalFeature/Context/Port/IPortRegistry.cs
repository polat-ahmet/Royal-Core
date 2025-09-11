using System;
using System.Collections.Generic;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context.Port
{
    public sealed class PortMeta
    {
        public string Id; // benzersiz veya anlamlı kimlik
        public string[] Tags; // filtreleme için (örn. "minigame","pve")
        public int Version = 1;
    }

    public interface IPortRegistry : IPort
    {
        // Kayıt yönetimi
        string Register<TPort>(TPort port, PortMeta meta = null) where TPort : class, IPort;
        bool Unregister(string id);

        // Çözümleme
        bool TryResolve<TPort>(out TPort port, Func<PortMeta, bool> predicate = null) where TPort : class, IPort;

        IReadOnlyList<(TPort port, PortMeta meta)> ResolveMany<TPort>(Func<PortMeta, bool> predicate = null)
            where TPort : class, IPort;
    }
}