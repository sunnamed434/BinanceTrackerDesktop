using BinanceTrackerDesktop.Core.Components.ContextMenuStripControl.Item.Control;
using BinanceTrackerDesktop.Core.Themes.Detectors;
using BinanceTrackerDesktop.Core.Themes.Models;
using BinanceTrackerDesktop.Core.Themes.Provider;
using BinanceTrackerDesktop.Core.Themes.Recognizers;
using static System.Windows.Forms.Control;

namespace BinanceTrackerDesktop.Core.Themes.Forms
{
    public sealed class FormsTheme
    {
        public static void Apply(MenuStrip menuStrip, IEnumerable<MenuStripComponentItemControl> items, ISystemThemeRecognizer themeRecognizer)
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

            IThemesProvider themesProvider = new ThemesProvider(new ThemeDetector(themeRecognizer));
            ThemeColors loadedThemeData = themesProvider.LoadThemeData();

            menuStrip.BackColor = loadedThemeData.MenuStrip;

            foreach (MenuStripComponentItemControl item in items)
            {
                item.SetDefaultTextColorAndAsCurrentForegroundColor(loadedThemeData.MenuStripItemText);
            }
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

            IThemesProvider themesProvider = new ThemesProvider(new ThemeDetector(themeRecognizer));
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
    }
}
