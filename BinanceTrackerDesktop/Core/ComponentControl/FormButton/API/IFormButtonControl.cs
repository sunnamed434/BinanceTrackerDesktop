using BinanceTrackerDesktop.Core.ComponentControl.API;
using BinanceTrackerDesktop.Core.Forms.API;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.ComponentControl.FormButton.API
{
    public interface IFormButtonControl : IFormClickEventListenerHandle, IFormTextColor
    {
        Button Button { get; }
    }

    public class FormButtonControl : FormComponentClickListener, IFormButtonControl
    {
        public Button Button { get; }

        public IFormEventListener ClickEvent => base.ClickEventListener;



        private Color defaultColor;



        public FormButtonControl(Button button, IFormEventListener clickEventListener) : base(clickEventListener)
        {
            if (button == null)
                throw new ArgumentNullException(nameof(button));

            Button = button;
            defaultColor = Color.Empty;
        }



        public void SetTextSync(string content, Color color = default)
        {
            if (content == null)
                throw new ArgumentNullException(nameof(content));

            SetText(content);

            if (color != default)
                SetTextColor(color);
        }

        public void SetText(string content)
        {
            if (string.IsNullOrEmpty(content))
                throw new ArgumentNullException(nameof(content));

            Button.Text = content;
        }

        public void SetTextColor(Color color)
        {
            if (color == Color.Empty)
                throw new ArgumentNullException(nameof(color));

            Button.ForeColor = color;
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
