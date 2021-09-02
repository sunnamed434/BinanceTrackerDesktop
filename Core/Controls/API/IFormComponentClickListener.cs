using BinanceTrackerDesktop.Forms.API;
using System;

namespace BinanceTrackerDesktop.Core.Controls.API
{
    interface IFormComponentClickListener
    {
        IFormEventListener ClickEventListener { get; }
    }

    public class FormComponentClickListener : IFormComponentClickListener
    {
        public IFormEventListener ClickEventListener { get; }



        public FormComponentClickListener(IFormEventListener clickEventListener)
        {
            if (clickEventListener == null)
                throw new ArgumentNullException(nameof(clickEventListener));

            ClickEventListener = clickEventListener;
        }
    }
}
