using BinanceTrackerDesktop.Core.Controllers;
using BinanceTrackerDesktop.Core.MVC.View;

namespace BinanceTrackerDesktop.Core.Views.Tracker
{
    public interface ITrackerView : IView<TrackerController>
    {
        string TotalBalanceText { get; set; }

        string TotalBalanceLossesText { get; set; }

        Color TotalBalanceLossesTextColor { get; set; }

        bool RefreshTotalBalanceButtonEnableState { get; set; }
    }
}
