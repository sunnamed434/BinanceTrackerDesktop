using BinanceTrackerDesktop.Core.Themes.Data.Models.String;

namespace BinanceTrackerDesktop.Core.Themes.Attributes
{
    [AttributeUsage(
        AttributeTargets.Field |
        AttributeTargets.Property,
        AllowMultiple = true)]
    public sealed class ThemeComponentAttribute : Attribute
    {
        public ThemeComponentNameStringModel ComponentNameStringModel { get; }



        public ThemeComponentAttribute(string name)
        {
            if (string.IsNullOrWhiteSpace(nameof(name)))
                throw new ArgumentException(nameof(name));

            ComponentNameStringModel = new ThemeComponentNameStringModel(name);
        }
    }
}
