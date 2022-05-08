namespace BinanceTrackerDesktop.Core.Themes.Models.HEX
{
    public sealed class ThemeComponenthColorModel
    {
        public string ColorText;



        public Color GetColor()
        {
            const string HEXMark = "#";
            string hex = ColorText.Replace(HEXMark, string.Empty);

            const int HexBaseNumberSystem = 16;
            
            Color color = Color.FromArgb(Convert.ToInt32(hex, HexBaseNumberSystem));
            return Color.FromArgb(color.R, color.G, color.B);
        }
    }
}
