using BinanceTrackerDesktop.Themes.Recognizers;
using BinanceTrackerDesktop.Themes.Recognizers.Provider;

namespace BinanceTrackerDesktop.Themes.Forms.Design
{
    public class DesignableForm : Form
    {
        public readonly ISystemThemeRecognizer SystemThemeRecognizer;



        public DesignableForm()
        {
            SystemThemeRecognizer = new SystemThemeRecognizerProvider().Recognize();
        }



        public void ApplyTheme(Form form, Control.ControlCollection controlCollection)
        {
            if (form == null)
            {
                throw new ArgumentNullException(nameof(form));
            }

            if (controlCollection == null)
            {
                throw new ArgumentNullException(nameof(controlCollection));
            }

            FormsTheme.Apply(form, controlCollection, SystemThemeRecognizer);
        }

        public void ApplyTheme(MenuStrip menuStrip, IEnumerable<KeyValuePair<byte, ToolStripMenuItem>> items)
        {
            if (menuStrip == null)
            {
                throw new ArgumentNullException(nameof(menuStrip));
            }

            if (items == null)
            {
                throw new ArgumentNullException(nameof(menuStrip));
            }

            if (items.Any() == false)
            {
                throw new InvalidOperationException();
            }

            FormsTheme.Apply(menuStrip, items, SystemThemeRecognizer);
        }

        public void ApplyTheme(IEnumerable<KeyValuePair<byte, ToolStripMenuItem>> items)
        {
            if (items == null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            if (items.Any() == false)
            {
                throw new InvalidOperationException();
            }

            FormsTheme.Apply(items, SystemThemeRecognizer);
        }
    }
}
