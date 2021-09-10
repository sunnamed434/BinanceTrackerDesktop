using BinanceTrackerDesktop.Core.Authorization;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Validation.Extension
{
    public static class ValidationExtension
    {
        public static Validator Rules(this TextBox source) => new Validator(source.Text);
    }
}
