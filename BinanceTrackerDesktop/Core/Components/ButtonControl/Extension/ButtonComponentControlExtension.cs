namespace BinanceTrackerDesktop.Core.Components.ButtonControl.Extension
{
    public static class ButtonComponentControlExtension
    {
        public static void LockButton(this ButtonComponentControl source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.Button.Enabled = false;
        }

        public static void UnlockButton(this ButtonComponentControl source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            source.Button.Enabled = true;
        }
    }
}
