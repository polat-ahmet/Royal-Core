using RoyalCoreDomain.Scripts.Framework.RoyalFeature.MVC.View;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services.ViewProvider
{
    public interface IViewProvider : IService
    {
        T LoadView<T>(string key) where T : Component, IView; // sync

        void Release(Object obj); // optional
        // İleride: Task<T> LoadViewAsync<T>(string key)
    }
}