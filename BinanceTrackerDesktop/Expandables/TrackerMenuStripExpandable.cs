using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;

namespace BinanceTrackerDesktop.Expandables;

public abstract class TrackerMenuStripExpandable : Expandable<TrackerMenuBase, byte>
{
    protected readonly MenuStrip MenuStrip;



    protected TrackerMenuStripExpandable(MenuStrip menuStrip)
    {
        MenuStrip = menuStrip ?? throw new ArgumentNullException(nameof(menuStrip));
    }



    public override void AddComponent(TrackerMenuBase item, byte id)
    {
        base.AddComponent(item, id);

        MenuStrip.Items.Add(item.ToolStripMenuItem);
    }

    public override void RemoveComponent(byte id)
    {
        base.RemoveComponent(id);

        MenuStrip.Items.Remove(GetComponentOrDefault(id).ToolStripMenuItem);
    }
}
