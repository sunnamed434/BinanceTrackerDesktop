namespace BinanceTrackerDesktop.Expandables;

public class MenuStripExpandableDesignable : ExpandableDesignable<ToolStripMenuItem, byte>
{
    public readonly MenuStrip MenuStrip;



    public MenuStripExpandableDesignable(MenuStrip menuStrip)
    {
        MenuStrip = menuStrip ?? throw new ArgumentNullException(nameof(menuStrip));
    }



    public override void AddComponent(ToolStripMenuItem item, byte id)
    {
        base.AddComponent(item, id);

        MenuStrip.Items.Add(item);
    }

    public override void RemoveComponent(byte id)
    {
        base.RemoveComponent(id);

        MenuStrip.Items.Remove(GetComponentOrDefault(id));
    }
}
