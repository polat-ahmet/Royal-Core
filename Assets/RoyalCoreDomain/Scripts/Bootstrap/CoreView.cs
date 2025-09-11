using RoyalCoreDomain.Scripts.Framework.RoyalFeature.MVC.View;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Bootstrap
{
    [DisallowMultipleComponent]
    public sealed class CoreView : MonoBehaviour, IView
    {
        [Header("Anchors")] public Transform Root; // opsiyonel

        public Transform DriversParent; // TickDriver gibi runtime Mono'lar buraya eklenir

        private void Reset()
        {
            if (Root == null) Root = transform;
            if (DriversParent == null) DriversParent = transform;
        }
    }
}