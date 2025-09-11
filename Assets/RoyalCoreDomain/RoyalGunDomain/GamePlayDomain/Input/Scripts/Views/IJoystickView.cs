using RoyalCoreDomain.Scripts.Framework.RoyalFeature.MVC.View;
using UnityEngine;

namespace RoyalCoreDomain.Scripts.Framework.Template.RoyalFeatureTemplate.Scripts.Views
{
    public interface IJoystickView : IView
    {
        RectTransform Outline { get; }
        RectTransform Knob { get; }
        void SetVisible(bool v);
    }
}