using BinanceTrackerDesktop.Expandables;
using BinanceTrackerDesktop.Themes.Forms;
using BinanceTrackerDesktop.Themes.Recognizers.Provider;
using BinanceTrackerDesktop.Views.Tracker.Menu.Base;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items;

namespace BinanceTrackerDesktop.Views.Tracker.Menu;

public sealed class TrackerMenuStripControlUI : TrackerMenuStripExpandable, IInitializableExpandable<TrackerMenuBase, byte>
{
    public TrackerMenuStripControlUI(MenuStrip menuStrip) : base(menuStrip)
    {
        MenuStrip.RenderMode = ToolStripRenderMode.Professional;

        AddComponents(this);
        FormsTheme.Apply(menuStrip, AllComponents, new SystemThemeRecognizerProvider().Recognize());
    }
    


    IEnumerable<KeyValuePair<byte, TrackerMenuBase>> IInitializableExpandable<TrackerMenuBase, byte>.InitializeItems()
    {
        yield return new KeyValuePair<byte, TrackerMenuBase>(MenuItemsIdContainer.API, new TrackerMenuAPI());
        yield return new KeyValuePair<byte, TrackerMenuBase>(MenuItemsIdContainer.Coins, new TrackerMenuCoins());
        yield return new KeyValuePair<byte, TrackerMenuBase>(MenuItemsIdContainer.Settings, new TrackerMenuSettings());
    }
}

public sealed class MenuItemsIdContainer
{
    public const byte API = 1;

    public const byte Coins = 2;

    public const byte Settings = 3;
}
