using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Ports;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Services
{
    public sealed class TargetingService : ITargetingService
    {
        private readonly LayerMask _losMask; // opsiyonel: "World" çarpışma maskesi
        private readonly ITargetRegistry _reg;

        public TargetingService(ITargetRegistry reg, LayerMask losMask = default)
        {
            _reg = reg;
            _losMask = losMask;
        }

        public bool TryGetNearest(Vector2 origin, float range, out ITargetable best)
        {
            best = null;
            var bestD2 = float.MaxValue;
            var r2 = range * range;

            foreach (var t in _reg.All)
            {
                if (t == null || !t.IsAlive) continue;
                var p = (Vector2)t.Transform.position;
                var d2 = (p - origin).sqrMagnitude;
                if (d2 > r2 || d2 >= bestD2) continue;

                // (Opsiyonel) line-of-sight
                if (_losMask != 0)
                {
                    var hit = Physics2D.Linecast(origin, p, _losMask);
                    if (hit.collider) continue; // arada duvar var
                }

                best = t;
                bestD2 = d2;
            }

            return best != null;
        }
    }
}