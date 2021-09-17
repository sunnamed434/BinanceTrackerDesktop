using BinanceTrackerDesktop.Core.Components.TextControl.API;
using BinanceTrackerDesktop.Core.Forms.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.ComponentControl.LabelControl.API
{
    public class LabelComponentEventsContainer
    {
        public IEventListener ClickEventListener { get; }



        public LabelComponentEventsContainer()
        {
            ClickEventListener = new EventListener();
        }
    }

    public class LabelComponentControl : TextComponentControl
    {
        public LabelComponentEventsContainer EventsContainer { get; }



        private readonly Label label;



        public LabelComponentControl(Label label) : base(label)
        {
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            EventsContainer = new LabelComponentEventsContainer();
            this.label = label;

            this.label.Click += (s, e) => EventsContainer.ClickEventListener.TriggerEvent(e);
        }
    }
}