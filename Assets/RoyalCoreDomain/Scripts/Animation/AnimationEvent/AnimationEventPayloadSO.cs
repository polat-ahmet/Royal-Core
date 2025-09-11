using UnityEngine;

namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Modules.Animation
{
    [CreateAssetMenu(fileName = "AnimationEventPayload", menuName = "Royal Core/Animation Event/AnimationEventPayload",
        order = 0)]
    public class AnimationEventPayloadSO : ScriptableObject
    {
        public string eventName;
    }
}