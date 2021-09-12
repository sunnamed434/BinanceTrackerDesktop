using BinanceTrackerDesktop.Core.ComponentControl.API;
using BinanceTrackerDesktop.Core.Forms.API;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.ComponentControl.FormText.API
{
    public interface IFormTextControl : IFormClickEventListenerHandle, IFormTextColor
    {
        Label Label { get; }
    }

    public class FormTextControl : FormComponentClickListener, IFormTextControl
    {
        public Label Label { get; }

        public IFormEventListener ClickEvent => base.ClickEventListener;



        private Color defaultColor;



        public FormTextControl(Label label, IFormEventListener onClickEventListener) : base(onClickEventListener)
        {
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            Label = label;
            defaultColor = Color.Empty;
        }



        public void SetTextSync(string content, Color color = default)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            SetText(content);

            if (color != default)
                SetTextColor(color);
        }

        public void SetText(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            Label.Text = content;
        }

        public void SetTextColor(Color color)
        {
            if (color == Color.Empty)
                throw new ArgumentNullException(nameof(color));

            Label.ForeColor = color;
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
