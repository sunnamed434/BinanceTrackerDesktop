namespace BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;

public abstract class TrackerMenuBase : ITrackerMenu
{
    public readonly ToolStripMenuItem ToolStripMenuItem;



    protected TrackerMenuBase()
    {
        ToolStripMenuItem = InitializeToolStripMenuItem() ?? throw new ArgumentNullException(nameof(InitializeToolStripMenuItem));
        ToolStripMenuItem.Click += (_, _) => OnClick();
    }



    public virtual string Label { get; }

    public virtual Image Image { get; } = null;

    public virtual ToolStripItem[] DropDownItems { get; } = null;



    public abstract void OnClick();

    protected virtual ToolStripMenuItem InitializeToolStripMenuItem()
    {
        return new ToolStripMenuItem(Label, Image, DropDownItems);
    }
}
