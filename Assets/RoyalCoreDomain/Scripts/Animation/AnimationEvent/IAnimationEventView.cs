using System;

namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Modules.Animation
{
    public interface IAnimationEventView
    {
        public Action<AnimationEventPayloadSO> _OnAnimationEventTriggered { get; }
        void OnAnimationEvent(AnimationEventPayloadSO animationEvent);
    }
}