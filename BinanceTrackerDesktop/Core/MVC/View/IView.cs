namespace BinanceTrackerDesktop.Core.MVC.View
{
    public interface IView<TController> where TController : class
    {
        void SetController(TController controller);
    }
}
