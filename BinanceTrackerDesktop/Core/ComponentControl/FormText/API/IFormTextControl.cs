using BinanceTrackerDesktop.Core.ComponentControl.API;
using BinanceTrackerDesktop.Core.Forms.API;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.ComponentControl.FormText.API
{
    public interface IFormTextControl : IFormTextColor
    {
        Label Label { get; }

        FormTextEventsContainer EventsContainer { get; }
    }

    public class FormTextEventsContainer
    {
        public IFormEventListener ClickEventListener { get; }



        public FormTextEventsContainer()
        {
            ClickEventListener = new FormEventListener();
        }
    }

    public class FormTextControl : IFormTextControl
    {
        public Label Label { get; }

        public FormTextEventsContainer EventsContainer { get; }



        private Color defaultColor;



        public FormTextControl(Label label)
        {
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            EventsContainer = new FormTextEventsContainer();
            Label = label;
            defaultColor = Color.Empty;

            Label.Click += (s, e) => EventsContainer.ClickEventListener.TriggerEvent(e);
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