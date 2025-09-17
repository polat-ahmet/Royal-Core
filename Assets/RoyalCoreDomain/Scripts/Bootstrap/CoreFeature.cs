using System.Threading;
using System.Threading.Tasks;
using RoyalCoreDomain.RoyalGunDomain.Scripts.Feature;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context.Port;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature.Builder;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services.ModelProvider;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services.ViewProvider;
using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Feature;
using RoyalCoreDomain.Scripts.Loading;
using RoyalCoreDomain.Scripts.Services.Audio;
using RoyalCoreDomain.Scripts.Services.CameraService;
using RoyalCoreDomain.Scripts.Services.Logger;
using RoyalCoreDomain.Scripts.Services.SceneService;
using RoyalCoreDomain.Scripts.Services.UI;
using RoyalCoreDomain.Scripts.Services.UpdateService;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Bootstrap
{
    public sealed class CoreFeature : BaseFeature
    {
        private CoreView _view;
        private CancellationTokenSource _ct;

        public CoreFeature(string address, CancellationTokenSource ct) : base(address)
        {
            _ct = ct;
        }

        protected override void OnPreInstall()
        {
            // PlanChild("GamePlay", (addr, p) => new GamePlayFeature(addr, p));
        }

        protected override void OnInstall()
        {
            Context.Services.Bind<IFeatureRegistry>(new FeatureRegistry());
            Context.Services.Bind<IFeatureFactory>(new FeatureFactory());

            var vp = new ResourcesViewProvider();
            Context.Services.Bind<IViewProvider>(vp);

            var mp = new ResourcesModelProvider();
            Context.Services.Bind<IModelProvider>(mp);

            Context.Services.Bind<ITimeService>(new UnityTimeService());

            var updateService = new UpdateService<IUpdatable>();
            Context.Services.Bind<IUpdateService<IUpdatable>>(updateService);
            Context.Services.Bind<IUpdateService<IFixedUpdatable>>(new UpdateService<IFixedUpdatable>());
            Context.Services.Bind<IUpdateService<ILateUpdatable>>(new UpdateService<ILateUpdatable>());

            var logger = new UnityLogger();

            Context.Services.Bind<ISceneLoaderService>(new SceneLoaderService());

            // (optional) global port registry
            var portReg = new DynamicPortRegistry();
            Context.Ports.Bind<IPortRegistry>(portReg);
            Context.Export<IPortRegistry>(portReg);


            _view = vp.LoadView<CoreView>("Core/CoreView");
            Context.Views.Bind(_view);

            var canvasView = vp.LoadView<UIRoot>("Core/UIRoot");
            Context.Services.Bind<IUIService>(new UIService(vp, canvasView));

            var audioView = vp.LoadView<AudioView>("Core/AudioView");
            var audioService = new AudioService(audioView);
            Context.Services.Bind<IAudioService>(audioService);

            audioService.AddAudioClips(mp.LoadSO<AudioClipsScriptableObject>("Core/CoreAudioClips"));

            var cameraView = vp.LoadView<CameraView>("Core/CameraView");
            var cameraService = new CinemachineCameraService();
            Context.Services.Bind<ICameraFollowService>(cameraService);

            cameraService.BindCamera(cameraView.Cam, cameraView.vCam);
        }

        protected override void OnStart()
        {
            var host = _view.DriversParent != null ? _view.DriversParent.gameObject : _view.gameObject;

            var driver = host.AddComponent<UpdateDriver>();
            driver.Initialize(Context.ImportService<ITimeService>(), 
                Context.ImportService<IUpdateService<IUpdatable>>(),
                Context.ImportService<IUpdateService<IFixedUpdatable>>(),
                Context.ImportService<IUpdateService<ILateUpdatable>>());

            Context.ImportService<ISceneLoaderService>().InitEntryPoint();

            
            _ = InitEntryPoint(_ct);
            

            // var cameraService = Context.ImportService<ICameraFollowService>();
            // lateUpdateService.RegisterUpdatable(cameraService);

            // var factory = Context.ImportService<IFeatureFactory>();
            // factory.Add(this, "GamePlay", (addr, p) => new GamePlayFeature(addr, p));
        }
        
        private async Awaitable InitEntryPoint(CancellationTokenSource ct)
        {
            Context.ImportService<IUIService>().PushPopup<LoadingView>("Core/LoadingView");
            var game = await Context.ImportService<IFeatureFactory>().CreateAsync(this, "Game",
                (addr, parent) => new GameFeature(addr, parent), ct);
            game.Start();
            Context.ImportService<IUIService>().PopPopup();
        }

        protected override void OnDispose()
        {
            if (Context.TryImportService<IViewProvider>(out var vp) && _view != null) vp.Release(_view);
            _view = null;
        }
    }
}