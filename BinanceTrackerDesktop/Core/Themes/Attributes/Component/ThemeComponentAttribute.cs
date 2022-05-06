using BinanceTrackerDesktop.Core.Themes.Models.String;

namespace BinanceTrackerDesktop.Core.Themes.Attributes.Component
{
    [AttributeUsage(AttributeTargets.Field)]
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
