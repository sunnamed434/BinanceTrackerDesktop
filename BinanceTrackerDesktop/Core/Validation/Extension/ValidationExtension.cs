using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Validation.Extension
{
    public static class ValidationExtension
    {
        public static StringValidator Rules(this TextBox source) => new StringValidator(source.Text);
    }
}
