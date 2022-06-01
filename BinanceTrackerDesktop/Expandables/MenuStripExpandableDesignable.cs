namespace BinanceTrackerDesktop.Expandables;

public class MenuStripExpandableDesignable : ExpandableDesignable<ToolStripMenuItem, byte>
{
    protected readonly MenuStrip MenuStrip;



    public MenuStripExpandableDesignable(MenuStrip menuStrip)
    {
        MenuStrip = menuStrip ?? throw new ArgumentNullException(nameof(menuStrip));
    }



    public override void AddComponent(ToolStripMenuItem item, byte search)
    {
        base.AddComponent(item, search);

        MenuStrip.Items.Add(item);
    }

    public override void RemoveComponent(byte search)
    {
        base.RemoveComponent(search);

        MenuStrip.Items.Remove(GetComponentOrDefault(search));
    }
}
