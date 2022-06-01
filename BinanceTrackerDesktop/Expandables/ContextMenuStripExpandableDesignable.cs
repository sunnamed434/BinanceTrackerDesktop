namespace BinanceTrackerDesktop.Expandables;

public class ContextMenuStripExpandableDesignable : ExpandableDesignable<ToolStripMenuItem, byte>
{
    protected readonly ContextMenuStrip ContextMenuStrip;



    public ContextMenuStripExpandableDesignable(ContextMenuStrip contextMenuStrip)
    {
        ContextMenuStrip = contextMenuStrip ?? throw new ArgumentNullException(nameof(contextMenuStrip));
    }



    public override void AddComponent(ToolStripMenuItem item, byte search)
    {
        base.AddComponent(item, search);

        ContextMenuStrip.Items.Add(item);
    }

    public override void RemoveComponent(byte search)
    {
        base.RemoveComponent(search);

        ContextMenuStrip.Items.Remove(GetComponentOrDefault(search));
    }
}
