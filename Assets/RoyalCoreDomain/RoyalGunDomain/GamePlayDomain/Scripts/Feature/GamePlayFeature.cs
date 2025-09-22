using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Agent.Weapon.Scripts.Bullet;
using RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Enemy.Scripts.Feature;
using RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Enemy.Scripts.Services;
using RoyalCoreDomain.RoyalGunDomain.GamePlayDomain.Scripts.Services.ControlledAgentService;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature.Builder;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services.ViewProvider;
using RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Services;
using RoyalCoreDomain.Scripts.Services.Audio;
using RoyalCoreDomain.Scripts.Services.Pool;
using RoyalCoreDomain.Scripts.Services.UpdateService;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Feature
{
    public class GamePlayFeature : SceneOwnerFeature
    {
        protected override string SceneKey => "GamePlayScene";
        private readonly GamePlayArgs _args;
        
        private IFeatureFactory _factory;
        
        public GamePlayFeature(string address, IFeature parent, GamePlayArgs args) : base(address, parent)
        {
            _args = args;
        }

        protected override void OnPreInstall()
        {
            // PlanChild("Player", (addr, p) => new PlayerFeature(addr, p));
            // PlanChild("Input", (addr, p) => new InputFeature(addr, p));
        }

        protected override void OnInstall()
        {
            var factory = Context.ImportService<IFeatureFactory>();

            Context.Services.Bind<IControlledAgentService>(new ControlledAgentService());

            var reg = new TargetRegistry();
            Context.Services.Bind<ITargetRegistry>(reg);

            var targeting = new TargetingService(reg, LayerMask.GetMask("World"));
            Context.Services.Bind<ITargetingService>(targeting);

            // Context.Services.Bind<IBulletFactory>(new BulletFactoryService(Context.ImportService<IViewProvider>()));
            
            Context.Services.Bind<IPoolRegistryService>(new PoolRegistryService());

            // var player = factory.Add(this, "player", (addr, p) => new PlayerFeature(addr, p));
            // factory.Add(this, "input", (addr, p) => new InputFeature(addr, p));

            //TODO get spawner data
            var spawnerPositions = new List<Vector2>();
            spawnerPositions.Add(new Vector2(5, 5));
            spawnerPositions.Add(new Vector2(-5, -5));
            var spawner = new EnemySpawnerService(this, factory, reg, spawnerPositions);
            Context.Services.Bind(spawner);
        }
        
        protected override void OnResolve()
        {
            base.OnResolve();
            _factory = Context.ImportService<IFeatureFactory>() ?? new FeatureFactory();
        }
        
        protected override async Task OnSceneReadyAsync(CancellationTokenSource ct)
        {
            // LevelData/SpawnConfig gibi sahneye bağlı çocukları burada kur
            // Örn: Player, EnemySpawner…
            // PlanChild("Player", (addr, p) => new PlayerFeature(addr, p));
            // PlanChild("Input", (addr, p) => new InputFeature(addr, p));
            var reg = Context.ImportService<IPoolRegistryService>();
            var vp = Context.ImportService<IViewProvider>();
            
            // Bullet View Pool
            var holder = new GameObject("BulletPool").transform;
            var bulletId = new PoolId("view", "Bullets/StandardBullet");
            
            var bulletPool = reg.GetOrCreate(bulletId, () =>
                new ViewPool<BulletView>(vp, "Bullets/StandardBullet", holder,
                    cfg: new PoolConfig{ InitialAmount = 64, MaxSize = 256 })
            );
            
            Context.Services.Bind<IBulletFactory>(new BulletFactoryService(bulletPool));
            
                
            await _factory.CreateAsync(this, "Player", (addr, parent) => new PlayerFeature(addr, parent), ct);
            await _factory.CreateAsync(this, "Input", (addr, parent) => new InputFeature(addr, parent), ct);
            // Spawner & HUD vs.
        }

        protected override void OnStart()
        {
            Context.ImportService<IAudioService>().PlayAudio(AudioClipType.GamePlayBgMusic, AudioChannelType.Music,
                AudioPlayType.Loop);
            Context.ImportService<IUpdateService<IUpdatable>>()
                .RegisterUpdatable(Context.ImportService<EnemySpawnerService>());
        }

        protected override void OnDispose()
        {
            Context.ImportService<IUpdateService<IUpdatable>>()
                .UnregisterUpdatable(Context.ImportService<EnemySpawnerService>());
        }
    }
}