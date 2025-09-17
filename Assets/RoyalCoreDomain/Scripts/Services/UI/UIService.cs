using System;
using System.Collections.Generic;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.MVC.View;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services.ViewProvider;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Services.UI
{
    public sealed class UIService : IUIService
    {
        private readonly Dictionary<UILayer, List<Component>> _byLayer = new();
        private readonly Stack<Component> _popupStack = new();
        private readonly IUIRoot _root;
        private readonly IViewProvider _views;

        public UIService(IViewProvider views, IUIRoot root)
        {
            _views = views;
            _root = root;
            foreach (UILayer l in Enum.GetValues(typeof(UILayer)))
                _byLayer[l] = new List<Component>();
        }

        public T Show<T>(string key, UILayer layer = UILayer.HUD) where T : Component, IView
        {
            var v = _views.LoadView<T>(key);

            // Tek Canvas altındaki ilgili layer paneline yerleştir
            var parent = _root.GetLayerRoot(layer);
            var rt = v.transform as RectTransform;
            if (rt != null)
            {
                rt.SetParent(parent, false);
                rt.anchorMin = Vector2.zero;
                rt.anchorMax = Vector2.one;
                rt.offsetMin = rt.offsetMax = Vector2.zero;
            }
            else
            {
                // UI değilse yine de parent et
                v.transform.SetParent(parent, false);
            }

            _byLayer[layer].Add(v);
            return v;
        }

        public void Close<T>(T view) where T : Component, IView
        {
            if (!view) return;

            // Katman listesinden çıkar
            foreach (var kv in _byLayer)
                if (kv.Value.Remove(view))
                    break;

            // ViewProvider'a iade
            _views.Release(view);
        }

        public void CloseAll(UILayer layer)
        {
            var list = _byLayer[layer];
            for (var i = list.Count - 1; i >= 0; --i)
            {
                var c = list[i];
                if (c) _views.Release(c);
            }

            list.Clear();
        }

        public T PushPopup<T>(string key) where T : Component, IView
        {
            var v = Show<T>(key, UILayer.Popup);
            _popupStack.Push(v);
            return v;
        }

        public void PopPopup()
        {
            if (_popupStack.Count == 0) return;
            Debug.Log("Popping popup");
            var v = _popupStack.Pop();
            // tip paramı yok; en geniş Close çağrısı yeterli
            if (v is IView)
            {
                Debug.Log("Popping popup view");
                // generic Close<T> çağırmak için reflection’a gerek yok;
                
                // layer listelerinden de düş
                foreach (var kv in _byLayer) kv.Value.Remove(v);
                
                // doğrudan ViewProvider.Release yeterli:
                _views.Release(v.gameObject);
            }
        }
    }
}