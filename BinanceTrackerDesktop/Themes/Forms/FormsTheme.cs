using BinanceTrackerDesktop.Themes.Detectors.Data;
using BinanceTrackerDesktop.Themes.Models;
using BinanceTrackerDesktop.Themes.Provider;
using BinanceTrackerDesktop.Themes.Recognizers;
using static System.Windows.Forms.Control;

namespace BinanceTrackerDesktop.Themes.Forms;

public sealed class FormsTheme
{
    public static void Apply(MenuStrip menuStrip, IEnumerable<KeyValuePair<byte, ToolStripMenuItem>> items, ISystemThemeRecognizer themeRecognizer)
    {
        if (menuStrip == null)
        {
            throw new ArgumentNullException(nameof(menuStrip));
        }

        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        if (items.Any() == false)
        {
            throw new InvalidOperationException();
        }

        IThemesProvider themesProvider = new ThemesProvider(new ThemeDataDetector(themeRecognizer));
        ThemeColors loadedThemeData = themesProvider.LoadThemeData();

        menuStrip.BackColor = loadedThemeData.MenuStrip;

        Apply(items, themeRecognizer);
    }

    public static void Apply(Form form, ControlCollection controls, ISystemThemeRecognizer themeRecognizer)
    {
        if (form == null)
        {
            throw new ArgumentNullException(nameof(form));
        }

        if (controls == null)
        {
            throw new ArgumentNullException(nameof(controls));
        }

        if (themeRecognizer == null)
        {
            throw new ArgumentNullException(nameof(themeRecognizer));
        }

        IThemesProvider themesProvider = new ThemesProvider(new ThemeDataDetector(themeRecognizer));
        ThemeColors loadedThemeData = themesProvider.LoadThemeData();
        form.BackColor = loadedThemeData.Form;
        foreach (Control control in controls)
        {
            if (control is Button button)
            {
                button.BackColor = loadedThemeData.Button;
                button.ForeColor = loadedThemeData.ButtonText;
                button.FlatAppearance.BorderSize = 0;
                button.FlatStyle = FlatStyle.Flat;
            }

            if (control is Label label)
            {
                label.ForeColor = loadedThemeData.Text;
            }
        }
    }

    public static void Apply(IEnumerable<KeyValuePair<byte, ToolStripMenuItem>> items, ISystemThemeRecognizer themeRecognizer)
    {
        if (items == null)
        {
            throw new ArgumentException(nameof(items));
        }

        if (items.Any() == false)
        {
            throw new InvalidOperationException();
        }

        IThemesProvider themesProvider = new ThemesProvider(new ThemeDataDetector(themeRecognizer));
        ThemeColors loadedThemeData = themesProvider.LoadThemeData();
        foreach (KeyValuePair<byte, ToolStripMenuItem> keyValuePairItem in items)
        {
            keyValuePairItem.Value.ForeColor = loadedThemeData.MenuStripItemText;
        }
    }
}
