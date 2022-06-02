namespace BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;

public abstract class TrackerMenuBase : ITrackerMenu
{
    public readonly ToolStripMenuItem ToolStripMenuItem;



    public TrackerMenuBase()
    {
        ToolStripMenuItem = new ToolStripMenuItem(Label, Image, Items);
        ToolStripMenuItem.Click += (_, _) => OnClick();
    }



    public abstract string Label { get; }

    public abstract Image Image { get; }

    public abstract ToolStripItem[] Items { get; }



    public abstract void OnClick();
}
