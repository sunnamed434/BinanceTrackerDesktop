using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Provider;
using BinanceTrackerDesktop.Core.Forms.Authorization;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.Window.Extension;
using BinanceTrackerDesktop.Tracker.Forms;
using System.Diagnostics;

namespace BinanceTrackerDesktop.Core.Entry
{
    public sealed class BinanceTrackerEntryPoint
    {
        internal static AwaitableComponentsProvider AwaitableComponentsProvider;



        public BinanceTrackerEntryPoint()
        {
            if (Process.GetCurrentProcess().TryGetArleadyStartedSimilarProcess(out Process anotherProcess))
            {
                anotherProcess.SetProcessWindowToForeground();
                return;
            }

            AwaitableComponentsProvider = new AwaitableComponentsProvider(new AwaitableComponentsObserver());
            AwaitableComponentsProvider.RegisterAwaitablesOnce();

            if (new BinaryUserDataSaveSystem().Read() == null)
            {
                Application.Run(new BinanceTrackerAuthorizationForm());
            }
            else
            {
                Application.Run(new BinanceTrackerForm());
            }
        }
    }
}
