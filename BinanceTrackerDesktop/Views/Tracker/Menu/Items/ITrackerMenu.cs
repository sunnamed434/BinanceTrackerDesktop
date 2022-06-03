namespace BinanceTrackerDesktop.Views.Tracker.Menu.Items;

public interface ITrackerMenu
{
    string Label { get; }

    Image Image { get; }

    ToolStripItem[] DropDownItems { get; }



    void OnClick();
}
