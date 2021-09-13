using BinanceTrackerDesktop.Core.ComponentControl.API;
using BinanceTrackerDesktop.Core.Forms.API;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.ComponentControl.FormButton.API
{
    public interface IFormButtonControl : IFormTextColor
    {
        Button Button { get; }

        FormButtonEventsContainer EventsContainer { get; }
    }

    public class FormButtonEventsContainer
    {
        public IFormEventListener ClickEventListener { get; }



        public FormButtonEventsContainer()
        {
            ClickEventListener = new FormEventListener();
        }
    }

    public class FormButtonControl : IFormButtonControl
    {
        public Button Button { get; }

        public FormButtonEventsContainer EventsContainer { get; }



        private Color defaultColor;



        public FormButtonControl(Button button)
        {
            if (button == null)
                throw new ArgumentNullException(nameof(button));

            EventsContainer = new FormButtonEventsContainer();
            Button = button;
            defaultColor = Color.Empty;

            button.Click += (s, e) => EventsContainer.ClickEventListener.TriggerEvent(e);
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
