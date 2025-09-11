using System;
using UnityEngine;

namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Modules.Animation
{
    public abstract class BaseAnimationEventView : MonoBehaviour, IAnimationEventView
    {
        protected IAnimationEventView masterAnimationEventView;

        public Action<AnimationEventPayloadSO> _OnAnimationEventTriggered { get; }

        public void OnAnimationEvent(AnimationEventPayloadSO animationEvent)
        {
            masterAnimationEventView?.OnAnimationEvent(animationEvent);
        }
    }
}