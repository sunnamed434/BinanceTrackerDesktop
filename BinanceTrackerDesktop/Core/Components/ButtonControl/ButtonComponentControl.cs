using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.TextControl;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.ButtonControl
{
    public sealed class ButtonEventsContainer
    {
        public readonly EventListener ClickEventListener;



        public ButtonEventsContainer()
        {
            ClickEventListener = new EventListener();
        }
    }

    public sealed class ButtonComponentControl : TextComponentControl
    {
        public readonly ButtonEventsContainer EventsContainer;

        public readonly Button Button;



        public ButtonComponentControl(Button button) : base(button)
        {
            if (button == null)
                throw new ArgumentNullException(nameof(button));

            EventsContainer = new ButtonEventsContainer();
            Button = button;

            Button.Click += (s, e) => EventsContainer.ClickEventListener.TriggerEvent(e);
        }
    }
}