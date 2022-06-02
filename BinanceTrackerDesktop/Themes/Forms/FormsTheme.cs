﻿using BinanceTrackerDesktop.Themes.Detectors.Data;
using BinanceTrackerDesktop.Themes.Models;
using BinanceTrackerDesktop.Themes.Provider;
using BinanceTrackerDesktop.Themes.Recognizers;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items;
using BinanceTrackerDesktop.Views.Tracker.Menu.Items.Base;
using static System.Windows.Forms.Control;

namespace BinanceTrackerDesktop.Themes.Forms;

public sealed class FormsTheme
{
    public static void Apply(MenuStrip menuStrip, IEnumerable<KeyValuePair<byte, ToolStripMenuItem>> items, ISystemThemeRecognizer systemThemeRecognizer)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        if (items.Any() == false)
        {
            throw new InvalidOperationException();
        }

        Apply(menuStrip, systemThemeRecognizer);
        Apply(items, systemThemeRecognizer);
    }

    public static void Apply(MenuStrip menuStrip, ISystemThemeRecognizer systemThemeRecognizer)
    {
        if (menuStrip == null)
        {
            throw new ArgumentNullException(nameof(menuStrip));
        }

        IThemesProvider themesProvider = new ThemesProvider(new ThemeDataDetector(systemThemeRecognizer));
        ThemeColors loadedThemeData = themesProvider.LoadThemeData();

        menuStrip.BackColor = loadedThemeData.MenuStrip;
    }

    public static void Apply(Form form, ControlCollection controls, ISystemThemeRecognizer systemThemeRecognizer)
    {
        if (form == null)
        {
            throw new ArgumentNullException(nameof(form));
        }

        if (controls == null)
        {
            throw new ArgumentNullException(nameof(controls));
        }

        if (systemThemeRecognizer == null)
        {
            throw new ArgumentNullException(nameof(systemThemeRecognizer));
        }

        IThemesProvider themesProvider = new ThemesProvider(new ThemeDataDetector(systemThemeRecognizer));
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

    public static void Apply(MenuStrip menuStrip, IEnumerable<KeyValuePair<byte, TrackerMenuBase>> items, ISystemThemeRecognizer systemThemeRecognizer)
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

        Apply(menuStrip, systemThemeRecognizer);

        IThemesProvider themesProvider = new ThemesProvider(new ThemeDataDetector(systemThemeRecognizer));
        ThemeColors loadedThemeData = themesProvider.LoadThemeData();
        foreach (KeyValuePair<byte, TrackerMenuBase> keyValuePairItem in items)
        {
            keyValuePairItem.Value.ToolStripMenuItem.ForeColor = loadedThemeData.MenuStripItemText;
        }
    }

    public static void Apply(IEnumerable<KeyValuePair<byte, ToolStripMenuItem>> items, ISystemThemeRecognizer systemThemeRecognizer)
    {
        if (items == null)
        {
            throw new ArgumentException(nameof(items));
        }

        if (items.Any() == false)
        {
            throw new InvalidOperationException();
        }

        IThemesProvider themesProvider = new ThemesProvider(new ThemeDataDetector(systemThemeRecognizer));
        ThemeColors loadedThemeData = themesProvider.LoadThemeData();
        foreach (KeyValuePair<byte, ToolStripMenuItem> keyValuePairItem in items)
        {
            keyValuePairItem.Value.ForeColor = loadedThemeData.MenuStripItemText;
        }
    }
}
