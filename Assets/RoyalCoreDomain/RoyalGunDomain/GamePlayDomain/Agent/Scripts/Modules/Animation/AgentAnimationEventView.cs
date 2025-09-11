using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Views;
using UnityEngine;

namespace RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Modules.Animation
{
    public class AgentAnimationEventView : BaseAnimationEventView
    {
        [SerializeField] private AgentView agentView;

        private void Awake()
        {
            masterAnimationEventView = agentView;
        }
    }
}