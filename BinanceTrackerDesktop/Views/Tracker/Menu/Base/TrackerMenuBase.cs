namespace BinanceTrackerDesktop.Views.Tracker.Menu.Base;

public abstract class TrackerMenuBase : ITrackerMenu
{
    public ToolStripMenuItem ToolStripMenuItem { get; protected set; }



    protected TrackerMenuBase()
    {
        ToolStripMenuItem = InitializeToolStripMenuItem() ?? throw new ArgumentNullException(nameof(InitializeToolStripMenuItem));
        ToolStripMenuItem.Click += (_, _) => OnClick();
    }



    public virtual string Label { get; } = string.Empty;

    public virtual Image Image { get; } = null;

    public virtual ToolStripItem[] DropDownItems { get; } = null;



    public abstract void OnClick();

    protected virtual ToolStripMenuItem InitializeToolStripMenuItem()
    {
        return new ToolStripMenuItem(Label, Image, DropDownItems);
    }
}
