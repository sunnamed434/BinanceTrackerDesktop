namespace BinanceTrackerDesktop.Core.Components.TextControl
{
    public class TextComponentControl : ITextable
    {
        private readonly Control control = null;

        private Color? defaultColor = null;



        public TextComponentControl(Control control, Color? defaultColor = null) : this(defaultColor)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            if (defaultColor != null)
                this.defaultColor = defaultColor;

            this.control = control;
        }

        public TextComponentControl(Color? defaultColor = null)
        {
            if (defaultColor != null)
                this.defaultColor = defaultColor;
        }

        public TextComponentControl()
        {
        }



        public virtual void SetText(string content, Color? color = null)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            if (color != null)
                SetForegroundColor(color);

            this.control.Text = content;
        }

        public virtual void SetForegroundColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            this.control.ForeColor = (Color)color;
        }

        public virtual void SetBackgroundColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            this.control.BackColor = (Color)color;
        }

        public virtual void SetDefaultTextColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            defaultColor = (Color)color;
        }

        public void SetDefaultTextColorAndAsCurrentForegroundColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            SetDefaultTextColor(color);
            SetForegroundColor(color);
        }

        public void SetDefaultTextColorAndAsCurrentBackgroundColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            SetDefaultTextColor(color);
            SetBackgroundColor(color);
        }

        public Color? GetDefaultTextColor()
        {
            return defaultColor;
        }
    }
}
