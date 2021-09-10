using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Forms.API
{
    public interface IFormSafelyComponentControl
    {
        IEnumerable<Func<Task>> Callbacks { get; }

        void RegisterListener(Func<Task> callback);

        Task CallListenersAsync();
    }

    public interface IFormStartedEvent
    {
        void OnStarted();
    }

    public interface IFormCompletedEvent
    {
        void OnCompleted();
    }

    public class FormSafelyComponentControl : IFormSafelyComponentControl
    {
        private readonly IFormStartedEvent startedEvent;

        private readonly IFormCompletedEvent completedEvent;



        private List<Func<Task>> closeCallbacks = new List<Func<Task>>();



        public IEnumerable<Func<Task>> Callbacks => closeCallbacks;



        public FormSafelyComponentControl(IFormStartedEvent startedEvent, IFormCompletedEvent completedEvent)
        {
            this.startedEvent = startedEvent;
            this.completedEvent = completedEvent;
        }

        public FormSafelyComponentControl(IFormStartedEvent startedEvent)
        {
            this.startedEvent = startedEvent;
        }

        public FormSafelyComponentControl(IFormCompletedEvent completedEvent)
        {
            this.completedEvent = completedEvent;
        }

        public FormSafelyComponentControl() : this(null, null)
        {

        }



        public void RegisterListener(Func<Task> callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            closeCallbacks.Add(callback);
        }

        public async Task CallListenersAsync()
        {
            this.startedEvent?.OnStarted();

            List<Task> callbackTasks = new List<Task>();
            foreach (Func<Task> callback in closeCallbacks)
            {
                if (callback == null)
                    throw new ArgumentNullException(nameof(callback));

                callbackTasks.Add(callback?.Invoke());
            }

            await Task.WhenAll(callbackTasks);

            this.completedEvent?.OnCompleted();
        }
    }
}