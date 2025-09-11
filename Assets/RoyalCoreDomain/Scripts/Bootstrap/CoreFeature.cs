using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context.Port;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature.Builder;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services.ModelProvider;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services.ViewProvider;
using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Feature;
using RoyalCoreDomain.Scripts.Services.Audio;
using RoyalCoreDomain.Scripts.Services.CameraService;
using RoyalCoreDomain.Scripts.Services.Logger;
using RoyalCoreDomain.Scripts.Services.UI;
using RoyalCoreDomain.Scripts.Services.UpdateService;

namespace RoyalCoreDomain.Scripts.Bootstrap
{
    public sealed class CoreFeature : BaseFeature
    {
        private CoreView _view;

        public CoreFeature(string address) : base(address)
        {
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

            Context.Services.Bind<ILogger>(new UnityLogger());


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
            var timeService = Context.ImportService<ITimeService>();
            var updateService = Context.ImportService<IUpdateService<IUpdatable>>();
            var fixedUpdateService = Context.ImportService<IUpdateService<IFixedUpdatable>>();
            var lateUpdateService = Context.ImportService<IUpdateService<ILateUpdatable>>();

            var host = _view.DriversParent != null ? _view.DriversParent.gameObject : _view.gameObject;

            var driver = host.AddComponent<UpdateDriver>();
            driver.Initialize(timeService, updateService, fixedUpdateService, lateUpdateService);

            // var cameraService = Context.ImportService<ICameraFollowService>();
            // lateUpdateService.RegisterUpdatable(cameraService);

            var factory = Context.ImportService<IFeatureFactory>();
            factory.Add(this, "game", (addr, p) => new GamePlayFeature(addr, p));
        }

        protected override void OnDispose()
        {
            if (Context.TryImportService<IViewProvider>(out var vp) && _view != null) vp.Release(_view);
            _view = null;
        }
    }
}