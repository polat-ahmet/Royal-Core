using System.Threading;
using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Services.SceneLoader
{
    public interface ISceneLoaderService : IService
    {
        void InitEntryPoint();
        // Awaitable<bool> TryLoadScene<TEnterData>(string sceneName, CancellationTokenSource cancellationTokenSource) where TEnterData : class, IInitiatorEnterData;
        // Awaitable StartScene<TEnterData>(SceneType gamePlayScene, TEnterData enterData, CancellationTokenSource cancellationTokenSource) where TEnterData : class, IInitiatorEnterData;
        Awaitable<bool> TryLoadScene(string sceneName, CancellationTokenSource cancellationTokenSource);
        Awaitable<bool> TryUnloadScene(string sceneName, CancellationTokenSource cancellationTokenSource);
    }
}