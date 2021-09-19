using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Components.ButtonControl.API
{
    public class ButtonComponentControl : TextComponentControl
    {
        public readonly ButtonEventsContainer EventsContainer;



        private readonly Button button;



        public ButtonComponentControl(Button button) : base(button)
        {
            if (button == null)
                throw new ArgumentNullException(nameof(button));

            EventsContainer = new ButtonEventsContainer();
            this.button = button;

            button.Click += (s, e) => EventsContainer.ClickEventListener.TriggerEvent(e);
        }

        

        public void Lock()
        {
            this.button.Enabled = false;
        }

        public void Unlock()
        {
            this.button.Enabled = true;
        }
    }

    public class ButtonEventsContainer
    {
        public readonly EventListener ClickEventListener;



        public ButtonEventsContainer()
        {
            ClickEventListener = new EventListener();
        }
    }
}