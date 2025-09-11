using UnityEngine;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.Services.ModelProvider
{
    public interface IModelProvider : IService
    {
        // ScriptableObject’ları yükle. Varsayılan: clone = true (asset üzerinde yazma hatalarını önler)
        T LoadSO<T>(string key, bool clone = true) where T : ScriptableObject;

        // İsteğe bağlı: Release (Resources için çoğu zaman gerekmez ama arayüzde dursun)
        void Release(Object obj);
    }
}