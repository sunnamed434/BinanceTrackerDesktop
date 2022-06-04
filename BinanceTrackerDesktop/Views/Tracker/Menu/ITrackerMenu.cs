namespace BinanceTrackerDesktop.Views.Tracker.Menu;

public interface ITrackerMenu
{
    string Label { get; }

    Image Image { get; }

    ToolStripItem[] DropDownItems { get; }



    void OnClick();
}
