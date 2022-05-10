namespace BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Item
{
    public interface IMenuStripItem
    {
        string Header { get; }

        Image Image { get; }

        byte Id { get; }
    }
}
