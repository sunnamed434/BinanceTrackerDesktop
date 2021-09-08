using BinanceTrackerDesktop.Core.Controls.API;
using BinanceTrackerDesktop.Forms.API;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Controls.FormButton
{
    public interface IFormButtonControl
    {
        Button Button { get; }
    }

    public class FormButtonControl : FormComponentClickListener, IFormButtonControl
    {
        public Button Button { get; }



        public FormButtonControl(Button button, IFormEventListener onClickEventListener) : base(onClickEventListener)
        {
            if (button == null) 
                throw new ArgumentNullException(nameof(button));

            if (onClickEventListener == null)
                throw new ArgumentNullException(nameof(onClickEventListener));
        }



        public abstract void OnClick();
    }
}
