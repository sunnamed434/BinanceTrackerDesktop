using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.ComponentControl.LabelControl.API
{
    public class LabelComponentControl : TextComponentControl
    {
        public readonly LabelComponentEventsContainer EventsContainer;



        private readonly Label label;



        public LabelComponentControl(Label label) : base(label)
        {
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            this.label = label;

            EventsContainer = new LabelComponentEventsContainer();
            this.label.MouseClick += (s, e) => EventsContainer.ClickEventListener.TriggerEvent(e);
        }
    }

    public class LabelComponentEventsContainer
    {
        public readonly EventListener ClickEventListener;



        public LabelComponentEventsContainer()
        {
            ClickEventListener = new EventListener();
        }
    }
}