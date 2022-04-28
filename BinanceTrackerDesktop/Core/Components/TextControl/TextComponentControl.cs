using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.TextControl
{
    public class TextComponentControl : ITextable
    {
        private readonly Control control;

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
            this.control = null;
        }



        public virtual void SetText(string content, Color? color = null)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            if (color != null)
                SetTextColor(color);

            this.control.Text = content;
        }

        public virtual void SetTextColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            this.control.ForeColor = (Color)color;
        }

        public void SetColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            this.control.ForeColor = (Color)color;
        }

        public void SetDefaultTextColor(Color? color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            defaultColor = (Color)color;
        }

        public Color? GetDefaultTextColor()
        {
            return defaultColor;
        }
    }
}
