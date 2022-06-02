namespace BinanceTrackerDesktop.Views.Tracker.Menu.Items;

public interface ITrackerMenu
{
    string Label { get; }

    Image Image { get; }

    ToolStripItem[] Items { get; }



    void OnClick();
}
