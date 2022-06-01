using BinanceTrackerDesktop.MVC.Controller;
using BinanceTrackerDesktop.MVC.View;
using BinanceTrackerDesktop.Window.Helper;

namespace BinanceTrackerDesktop.Controllers;

public sealed class TrackerTrayController : Controller<TrackerTrayController>
{
    private readonly IProcessWindowHelper processWindowHelper;



    public TrackerTrayController(IView<TrackerTrayController> view) : base(view)
    {
        processWindowHelper = new ProcessWindowHelper();
    }



    public void SetProccessWindowToForeground()
    {
        processWindowHelper.SetWindowToForeground();
    }



    protected override TrackerTrayController InitializeController()
    {
        return this;
    }
}
