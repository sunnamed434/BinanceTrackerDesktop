using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Forms.API
{
    public interface IFormSafelyCloseControl
    {
        IEnumerable<Func<Task>> Callbacks { get; }

        void RegisterListener(Func<Task> callback);

        Task CloseApplicationSafelyAndNotifyListenersAsync();
    }

    public class FormSafelyCloseControl : IFormSafelyCloseControl
    {
        private List<Func<Task>> closeCallbacks = new List<Func<Task>>();



        public IEnumerable<Func<Task>> Callbacks => closeCallbacks;



        public void RegisterListener(Func<Task> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            closeCallbacks.Add(callback);
        }

        public async Task CloseApplicationSafelyAndNotifyListenersAsync()
        {
            if (closeCallbacks.Count < 0)
                throw new InvalidOperationException();

            List<Task> callbackTasks = new List<Task>();
            foreach (Func<Task> callback in closeCallbacks)
            {
                if (callback == null)
                    throw new ArgumentNullException(nameof(callback));

                callbackTasks.Add(callback?.Invoke());
            }

            await Task.WhenAll(callbackTasks);

            Environment.Exit(0);
        }
    }
}
