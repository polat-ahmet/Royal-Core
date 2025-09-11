using System;
using System.Collections.Generic;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Feature
{
    public abstract class BaseFeature : IFeature
    {
        private readonly List<IFeature> _children = new(8);

        protected BaseFeature(string address, IFeature parent = null)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Parent = parent;
            Context = new FeatureContext((parent as BaseFeature)?.Context);
            Parent?.AddChild(this);
        }

        public string Address { get; protected set; }
        public IFeature Parent { get; private set; }
        public IReadOnlyList<IFeature> Children => _children;
        public FeatureState State { get; private set; } = FeatureState.Created;
        public FeatureContext Context { get; }

        public void Install()
        {
            if (State != FeatureState.Created) return;
            OnInstall();
            State = FeatureState.Installed;

            if (Context.TryImportService<IFeatureRegistry>(out var reg))
                reg.Register(this);

            foreach (var c in _children) c.Install();
        }

        public void Start()
        {
            if (State != FeatureState.Installed && State != FeatureState.Stopped) return;
            OnStart();
            State = FeatureState.Active;
            foreach (var c in _children) c.Start();
        }

        public void Pause()
        {
            if (State != FeatureState.Active) return;
            OnPause();
            State = FeatureState.Paused;
            foreach (var c in _children) c.Pause();
        }

        public void Resume()
        {
            if (State != FeatureState.Paused) return;
            OnResume();
            State = FeatureState.Active;
            foreach (var c in _children) c.Resume();
        }

        public void Stop()
        {
            if (State != FeatureState.Active && State != FeatureState.Paused) return;
            foreach (var c in _children) c.Stop();
            OnStop();
            State = FeatureState.Stopped;
        }

        public void Dispose()
        {
            if (State == FeatureState.Disposed) return;
            for (var i = _children.Count - 1; i >= 0; i--) _children[i].Dispose();
            OnDispose();

            // Context temizliği (SRP)
            Context.Services.Clear();
            Context.Controllers.Clear();
            Context.Models.Clear();
            Context.Views.Clear();
            Context.Ports.Clear();
            Context.PortDirectory.Clear();
            Context.Lists.Clear();

            if (Context.TryImportService<IFeatureRegistry>(out var reg))
                reg.Unregister(Address);

            State = FeatureState.Disposed;
        }

        public void AddChild(IFeature child)
        {
            if (child == null) throw new ArgumentNullException(nameof(child));
            if (_children.Contains(child)) return;
            (child as BaseFeature)!.Parent = this;
            _children.Add(child);
        }

        public void RemoveChild(IFeature child)
        {
            if (child == null) return;
            _children.Remove(child);
            (child as BaseFeature)!.Parent = null;
        }

        protected abstract void OnInstall();

        protected virtual void OnStart()
        {
        }

        protected virtual void OnPause()
        {
        }

        protected virtual void OnResume()
        {
        }

        protected virtual void OnStop()
        {
        }

        protected virtual void OnDispose()
        {
        }
    }
}