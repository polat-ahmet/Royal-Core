using System.Threading;
using System.Threading.Tasks;
using RoyalCoreDomain.RoyalGunDomain.LobbyDomain.Scripts.View;
using RoyalCoreDomain.RoyalGunDomain.Scripts.Ports;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services.ViewProvider;
using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Feature;
using RoyalCoreDomain.Scripts.Services.UI;
using UnityEngine;

namespace RoyalCoreDomain.RoyalGunDomain.LobbyDomain.Scripts
{
    public class LobbyFeature : SceneOwnerFeature
    {
        private LobbyView _view;
        private bool _navigating;
        protected override string SceneKey => "LobbyScene";
        
        
        public LobbyFeature(string address, IFeature parent) : base(address, parent)
        {
        }
 
        protected override Task OnSceneReadyAsync(CancellationTokenSource ct)
        {
            _view = Context.ImportService<IUIService>().Show<LobbyView>("Lobby/LobbyView");
            return Task.CompletedTask;
        }

        protected override void OnStart()
        {
            _view.SetupCallback(OnPlayClicked);
        }

        private void OnPlayClicked()
        {
            if (_navigating) return; 
            _navigating = true;
            
            var nav = Context.ImportPort<IGameNavPort>();
            _ = nav.OpenGameplayAsync(new GamePlayArgs{ LevelIndex = 1});
        }

        protected override void OnDispose()
        {
            if (Context.TryImportService<IUIService>(out var uiService) && _view != null) uiService.Close(_view);
            _view = null;
            base.OnDispose();
        }
    }
}