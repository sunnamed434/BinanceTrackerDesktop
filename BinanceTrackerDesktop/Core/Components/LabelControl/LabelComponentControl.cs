using BinanceTrackerDesktop.Core.Components.LabelControl.EventsContainer;
using BinanceTrackerDesktop.Core.Components.TextControl;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.ComponentControl.LabelControl
{
    public sealed class LabelComponentControl : TextComponentControl
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