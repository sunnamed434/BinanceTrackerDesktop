using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Validator.String.Extension
{
    public static class StringValidatorExtension
    {
        public static StringValidator Rules(this TextBox source) => new StringValidator(source.Text);

        public static StringValidator Rules(this string source) => new StringValidator(source);
    }
}
