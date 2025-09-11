using UnityEngine;

namespace RoyalCoreDomain.Scripts.Services.UpdateService
{
    public sealed class UnityTimeService : ITimeService
    {
        public float DeltaTime => Time.deltaTime;
        public float FixedDeltaTime => Time.fixedDeltaTime;
        public float UnscaledDeltaTime => Time.unscaledDeltaTime;

        public float TimeScale
        {
            get => Time.timeScale;
            set => Time.timeScale = value;
        }
    }
}