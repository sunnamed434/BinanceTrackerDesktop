using System.Text;

namespace BinanceTrackerDesktop.Core.Formatters.ValueString
{
    public sealed class ValueStringFormatter : IFormatter<decimal>
    {
        private const string DefaultFormat = "0.00";



        public object Format(decimal value)
        {
            return new StringBuilder()
                .Append(value.ToString(DefaultFormat))
                .ToString();
        }
    }
}
