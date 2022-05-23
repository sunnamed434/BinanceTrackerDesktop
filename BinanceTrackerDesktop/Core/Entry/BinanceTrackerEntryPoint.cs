using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Observer;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Provider;
using BinanceTrackerDesktop.Core.Controllers;
using BinanceTrackerDesktop.Core.Forms.Authorization;
using BinanceTrackerDesktop.Core.User.Client;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Status.Detector;
using BinanceTrackerDesktop.Core.Window.Extension;
using BinanceTrackerDesktop.Tracker.Forms;
using System.Diagnostics;

namespace BinanceTrackerDesktop.Core.Entry
{
    public sealed class BinanceTrackerEntryPoint
    {
        internal static AwaitablesProvider AwaitablesProvider;



        public BinanceTrackerEntryPoint()
        {
            if (Process.GetCurrentProcess().TryGetArleadyStartedSimilarProcess(out Process anotherProcess))
            {
                anotherProcess.SetProcessWindowToForeground();
                return;
            }

            AwaitablesProvider = new AwaitablesProvider(new AwaitablesObserver());
            AwaitablesProvider.RegisterAwaitablesOnce();

            if (new BinaryUserDataSaveSystem().Read() == null)
            {
                TrackerAuthorizationFormView authorizationFormView = new TrackerAuthorizationFormView();
                new AuthorizationController(authorizationFormView);
                authorizationFormView.ShowDialog();
            }
            else
            {
                TrackerFormView trackerFormView = new TrackerFormView();

                UserClient userClient = new UserClient();
                new TrackerController(trackerFormView, new UserStatusDetector(userClient.SaveDataSystem, userClient.Wallet).GetStatus());
                trackerFormView.ShowDialog();
            }
        }
    }
}
