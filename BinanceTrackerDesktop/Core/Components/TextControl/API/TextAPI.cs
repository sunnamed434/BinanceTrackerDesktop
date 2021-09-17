using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.TextControl.API
{
    public interface IText
    {
        void SetText(string content);
    }

    public interface ITextColor : IText
    {
        void SetTextColor(Color color);

        void SetDefaultTextColor(Color color);

        Color GetDefaultTextColor();
    }

    public interface ITextColorControl : IText, ITextColor
    {

    }

    public class TextComponentControl : ITextColorControl
    {
        private readonly Control control;

        private Color defaultColor = Color.Empty;



        public TextComponentControl(Control control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            this.control = control;
        }

        public TextComponentControl()
        {
            this.control = default;
        }



        public virtual void SetText(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            this.control.Text = content;
        }

        public virtual void SetTextColor(Color color)
        {
            if (color == Color.Empty)
                throw new ArgumentNullException(nameof(color));

            this.control.ForeColor = color;
        }

        public virtual void SetTextAndColor(string content, Color color)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            if (color == Color.Empty)
                throw new ArgumentNullException(nameof(color));

            SetText(content);
            SetTextColor(color);
        }

        public void SetDefaultTextColor(Color color)
        {
            if (color == Color.Empty)
                throw new ArgumentNullException(nameof(color));

            defaultColor = color;
        }

        public Color GetDefaultTextColor()
        {
            return defaultColor;
        }
    }
}
