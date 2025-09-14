using RoyalCoreDomain.Scripts.Framework.RoyalFeature.Context;

namespace RoyalCoreDomain.Scripts.Framework.RoyalFeature.MVC.Controller
{
    // public abstract class BaseController<TModel, TView> : IController where TModel : IModel where TView : IView
    // {
    //     protected TModel _model;
    //     protected TView _view;
    //
    //     public BaseController(TModel model, TView view)
    //     {
    //         _model = model;
    //         _view = view;
    //     }
    // }
    public abstract class BaseController : IController, IResolvable
    {
        public virtual void Dispose()
        {
        }

        public virtual void Resolve(FeatureContext context)
        {
        }
    }
}