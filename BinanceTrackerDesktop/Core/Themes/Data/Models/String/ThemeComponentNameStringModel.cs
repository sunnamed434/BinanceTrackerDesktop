namespace BinanceTrackerDesktop.Core.Themes.Data.Models.String
{
    public sealed class ThemeComponentNameStringModel
    {
        public string Name { get; set; }



        public ThemeComponentNameStringModel(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException(nameof(name));

            Name = name;
        }
    }
}
