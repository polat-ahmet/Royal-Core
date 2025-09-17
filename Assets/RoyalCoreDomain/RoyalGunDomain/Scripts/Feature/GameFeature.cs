using System.Threading;
using System.Threading.Tasks;
using RoyalCoreDomain.RoyalGunDomain.LobbyDomain.Scripts;
using RoyalCoreDomain.RoyalGunDomain.Scripts.Ports;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature.Builder;
using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Feature;
using RoyalCoreDomain.Scripts.Loading;
using RoyalCoreDomain.Scripts.Services.UI;
using UnityEngine;

namespace RoyalCoreDomain.RoyalGunDomain.Scripts.Feature
{
    public class GameFeature : SceneOwnerFeature, IGameNavPort
    {
        protected override string SceneKey => "GameScene";
        private LobbyFeature _lobbyFeature;
        private GamePlayFeature _gamePlayFeature;
        private CancellationTokenSource _ct;
 
        public GameFeature(string address, IFeature parent) : base(address, parent)
        {
        }

        protected override void OnInstall()
        {
            Context.Export<IGameNavPort>(this);
        }

        protected override async Task OnSceneReadyAsync(CancellationTokenSource ct)
        {
            _ct = ct;
            await OpenLobbyAsync();
        }

        protected override void OnStart()
        {
            Debug.Log("Game Feature OnStart");
        }

        public async Task OpenLobbyAsync()
        {
            var factory = Context.ImportService<IFeatureFactory>();
            
            factory.Remove(_gamePlayFeature);
                
            _lobbyFeature = await factory.CreateAsync(this, "Lobby", 
                (addr, parent) => new LobbyFeature(addr, parent), _ct);
            
            Debug.Log("GameFeature opened");
        }

        public async Task OpenGameplayAsync(GamePlayArgs args)
        {
            Context.ImportService<IUIService>().PushPopup<LoadingView>("Core/LoadingView");
            
            var factory = Context.ImportService<IFeatureFactory>();
            
            factory.Remove(_lobbyFeature);
            
            Debug.Log("Gameplay opening");
            
           _gamePlayFeature = await Context.ImportService<IFeatureFactory>().CreateAsync(this, "GamePlay", 
                (addr, parent) => new GamePlayFeature(addr, parent, new GamePlayArgs
                {
                    LevelIndex = 1
                }), _ct);
           
           Debug.Log("Gameplay opened");
           _gamePlayFeature.Start();
           
           Context.ImportService<IUIService>().PopPopup();
        }
    }
}