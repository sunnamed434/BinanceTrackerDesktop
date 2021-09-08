using BinanceTrackerDesktop.Forms.API;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms.SystemTray.API
{
    public interface IFormSystemTrayControl
    {
        NotifyIcon NotifyIcon { get; }

        IFormSafelyComponentControl SafelyComponentControl { get; }



        void AddItem(IFormTrayItemControl control);

        void RemoveItem(IFormTrayItemControl control);

        Task CloseAsync();
    }

    public class FormSystemTrayControl : IFormSystemTrayControl
    {
        public NotifyIcon NotifyIcon { get; }

        public IFormSafelyComponentControl SafelyComponentControl { get; }



        public FormSystemTrayControl(NotifyIcon notifyIcon)
        {
            if (notifyIcon == null)
                throw new ArgumentNullException(nameof(notifyIcon));

            NotifyIcon = notifyIcon;
            SafelyComponentControl = new FormSafelyCloseComponentControl();
        }



        public void AddItem(IFormTrayItemControl control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            NotifyIcon.ContextMenuStrip.Items.Add(control.ToolStrip);
        }

        public void RemoveItem(IFormTrayItemControl control)
        {
            if (control == null)
                throw new ArgumentNullException(nameof(control));

            NotifyIcon.ContextMenuStrip.Items.Remove(control.ToolStrip);
        }

        public async Task CloseAsync()
        {
            await SafelyComponentControl.CallListenersAsync();

            using (NotifyIcon)
                NotifyIcon.Visible = false;
        }
    }
}
