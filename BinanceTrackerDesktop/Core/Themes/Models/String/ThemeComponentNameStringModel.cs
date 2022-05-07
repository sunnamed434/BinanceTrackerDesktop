namespace BinanceTrackerDesktop.Core.Themes.Models.String
{
    public sealed class ThemeComponentNameStringModel
    {
        public string Name;



        public ThemeComponentNameStringModel(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            Name = name;
        }
    }
}
