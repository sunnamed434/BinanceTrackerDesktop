namespace BinanceTrackerDesktop.Expandables;

public class ContextMenuStripExpandableDesignable : ExpandableDesignable<ToolStripMenuItem, byte>
{
    public readonly ContextMenuStrip ContextMenuStrip;



    public ContextMenuStripExpandableDesignable(ContextMenuStrip contextMenuStrip)
    {
        ContextMenuStrip = contextMenuStrip ?? throw new ArgumentNullException(nameof(contextMenuStrip));
    }



    public override void AddComponent(ToolStripMenuItem item, byte id)
    {
        base.AddComponent(item, id);

        ContextMenuStrip.Items.Add(item);
    }

    public override void RemoveComponent(byte id)
    {
        base.RemoveComponent(id);

        ContextMenuStrip.Items.Remove(GetComponentOrDefault(id));
    }
}
