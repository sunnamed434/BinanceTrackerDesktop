namespace BinanceTrackerDesktop.Core.Themes.Models.RGB
{
    public sealed class ThemeComponentRGBModel
    {
        public int R;
                    
        public int G;
                    
        public int B;



        public Color GetColor() => Color.FromArgb(R, G, B);
    }
}
