using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.Components.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.ComponentControl.LabelControl.API
{
    public class LabelComponentEventsContainer
    {
        public readonly EventListener ClickEventListener;



        public LabelComponentEventsContainer()
        {
            ClickEventListener = new EventListener();
        }
    }

    public class LabelComponentControl : TextComponentControl
    {
        public readonly LabelComponentEventsContainer EventsContainer;

        public readonly Label Label;



        public LabelComponentControl(Label label) : base(label)
        {
            if (label == null)
                throw new ArgumentNullException(nameof(label));

            EventsContainer = new LabelComponentEventsContainer();
            Label = label;

            Label.MouseClick += (s, e) => EventsContainer.ClickEventListener.TriggerEvent(e);
        }
    }
}