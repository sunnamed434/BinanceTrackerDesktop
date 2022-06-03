using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;

namespace BinanceTrackerDesktop.Expandables
{
    public class TrackerContextMenuStripExpandable : Expandable<TrackerMenuBase, byte>
    {
        public readonly ContextMenuStrip ContextMenuStrip;



        public TrackerContextMenuStripExpandable(ContextMenuStrip contextMenuStrip)
        {
            ContextMenuStrip = contextMenuStrip ?? throw new ArgumentNullException(nameof(contextMenuStrip));
        }



        public override void AddComponent(TrackerMenuBase item, byte id)
        {
            base.AddComponent(item, id);

            ContextMenuStrip.Items.Add(item.ToolStripMenuItem);
        }

        public override void RemoveComponent(byte id)
        {
            base.RemoveComponent(id);

            ToolStripMenuItem toolStripMenuItem = null;
            if ((toolStripMenuItem = GetComponentOrDefault(id).ToolStripMenuItem) == null)
            {
                throw new ArgumentOutOfRangeException(nameof(id));
            }

            ContextMenuStrip.Items.Remove(toolStripMenuItem);
        }
    }
}
