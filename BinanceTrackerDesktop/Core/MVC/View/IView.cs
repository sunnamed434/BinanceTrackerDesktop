namespace BinanceTrackerDesktop.Core.MVC.View
{
    public interface IView<TController>
    {
        void SetController(TController controller);
    }
}
