using BinanceTrackerDesktop.Core.MVC.View;

namespace BinanceTrackerDesktop.Core.MVC.Controller
{
    public abstract class Controller<TSelf> where TSelf : class
    {
        protected Controller(IView<TSelf> view)
        {
            if (view == null)
            {
                throw new ArgumentNullException(nameof(view));
            }

            TSelf self = InitializeController();
            if (self == null)
            {
                throw new ArgumentNullException(nameof(TSelf));
            }

            view.SetController(self);
        }



        protected abstract TSelf InitializeController();
    }
}
