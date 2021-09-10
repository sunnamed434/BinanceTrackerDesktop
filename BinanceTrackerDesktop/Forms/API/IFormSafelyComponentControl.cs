using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Forms.API
{
    public interface IFormSafelyComponentControl : ICallbacksControl
    {
        IEnumerable<Func<Task>> Callbacks { get; }

        void RegisterListener(Func<Task> callback);

        Task<ICallbacksControl> CallListenersAsync();
    }

    public interface ICallbacksControl
    {
        IFormSafelyComponentControl OnStarted(Action callback);

        IFormSafelyComponentControl OnCompleted(Action callback);
    }

    public class FormSafelyComponentControl : IFormSafelyComponentControl
    {
        private Action onStartedCallback = null;

        private Action onCompletedCallback = null;



        private List<Func<Task>> closeCallbacks = new List<Func<Task>>();



        public IEnumerable<Func<Task>> Callbacks => closeCallbacks;



        public void RegisterListener(Func<Task> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            closeCallbacks.Add(callback);
        }

        public async Task<ICallbacksControl> CallListenersAsync()
        {
            onStartedCallback?.Invoke();

            List<Task> callbackTasks = new List<Task>();
            foreach (Func<Task> callback in closeCallbacks)
            {
                if (callback == null)
                    throw new ArgumentNullException(nameof(callback));

                callbackTasks.Add(callback?.Invoke());
            }

            await Task.WhenAll(callbackTasks);

            onCompletedCallback?.Invoke();

            return this;
        }

        public IFormSafelyComponentControl OnStarted(Action callback)
        {
            onStartedCallback = callback;
            return this;
        }

        public IFormSafelyComponentControl OnCompleted(Action callback)
        {
            onCompletedCallback = callback;
            return this;
        }
    }
}