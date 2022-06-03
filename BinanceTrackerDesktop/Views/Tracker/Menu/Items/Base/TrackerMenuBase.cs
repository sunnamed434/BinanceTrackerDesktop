namespace BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;

public abstract class TrackerMenuBase : ITrackerMenu
{
    public readonly ToolStripMenuItem ToolStripMenuItem;



    public TrackerMenuBase()
    {
        ToolStripMenuItem = new ToolStripMenuItem(Label, Image, DropDownItems);
        ToolStripMenuItem.Click += (_, _) => OnClick();
    }



    public abstract string Label { get; }

    public virtual Image Image { get; } = null;

    public virtual ToolStripItem[] DropDownItems { get; } = null;



    public abstract void OnClick();
}
