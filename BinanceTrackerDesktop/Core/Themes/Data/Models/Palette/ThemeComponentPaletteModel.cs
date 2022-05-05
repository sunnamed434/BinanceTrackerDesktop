namespace BinanceTrackerDesktop.Core.Themes.Data.Models.Palette
{
    public sealed class ThemeComponentPaletteModel
    {
        int R { get; }

        int G { get; }

        int B { get; }



        public Color GetColor() => Color.FromArgb(R, G, B);
    }
}
