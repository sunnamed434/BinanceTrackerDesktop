using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.MVC.View;

namespace BinanceTrackerDesktop.Views.Tracker;

public interface ITrackerView : IView<TrackerController>
{
    string TotalBalanceText { get; set; }

    string TotalBalanceLossesText { get; set; }

    Color TotalBalanceLossesTextColor { get; set; }

    bool RefreshTotalBalanceButtonEnableState { get; set; }
}
