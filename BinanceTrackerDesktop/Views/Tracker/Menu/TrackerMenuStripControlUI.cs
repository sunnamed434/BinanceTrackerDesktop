using BinanceTrackerDesktop.Expandables;
using BinanceTrackerDesktop.Themes.Forms;
using BinanceTrackerDesktop.Themes.Recognizers.Provider;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items.API;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Coins;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Settings;

namespace BinanceTrackerDesktop.Views.Tracker.Menu;

public sealed class TrackerMenuStripControlUI : TrackerMenuStripExpandable, IInitializableExpandable<TrackerMenuBase, byte>
{
    private readonly MenuStrip menuStrip;



    public TrackerMenuStripControlUI(MenuStrip menuStrip) : base(menuStrip)
    {
        if (menuStrip == null)
        {
            throw new ArgumentNullException(nameof(menuStrip));
        }

        this.menuStrip = menuStrip;
        this.menuStrip.RenderMode = ToolStripRenderMode.Professional;

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
