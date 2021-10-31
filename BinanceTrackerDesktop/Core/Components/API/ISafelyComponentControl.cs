using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Core.Components.API
{
    public interface ISafelyComponentControl
    {
        IEnumerable<Func<Task>> Callbacks { get; }



        void RegisterListener(Func<Task> callback);

        Task CallListenersAsync();

        ISafelyComponentControl OnCompleted(Action callback);

        ISafelyComponentControl OnStarted(Action callback);
    }

    public sealed class SafelyComponentControl : ISafelyComponentControl
    {
        private List<Func<Task>> closeCallbacks = new List<Func<Task>>();

        private List<Action> startCallbacks = new List<Action>();

        private List<Action> completeCallbacks = new List<Action>();



        public IEnumerable<Func<Task>> Callbacks => closeCallbacks;



        public void RegisterListener(Func<Task> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            closeCallbacks.Add(callback);
        }

        public async Task CallListenersAsync()
        {
            foreach (Action callback in startCallbacks)
            {
                callback?.Invoke();
            }

            List<Task> callbackTasks = new List<Task>();
            foreach (Func<Task> callback in closeCallbacks)
            {
                if (callback == null)
                    throw new ArgumentNullException(nameof(callback));

                callbackTasks.Add(callback?.Invoke());
            }

            await Task.WhenAll(callbackTasks);

            foreach (Action callback in completeCallbacks)
            {
                callback?.Invoke();
            }

            await Task.CompletedTask;
        }

        public ISafelyComponentControl OnStarted(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            startCallbacks.Add(callback);
            return this;
        }

        public ISafelyComponentControl OnCompleted(Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            completeCallbacks.Add(callback);
            return this;
        }
    }
}