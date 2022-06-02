using BinanceTrackerDesktop.Awaitable.Observer;
using BinanceTrackerDesktop.Controllers;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Provider;
using BinanceTrackerDesktop.Forms.Authorization;
using BinanceTrackerDesktop.Tracker.Forms;
using BinanceTrackerDesktop.User.Client;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.User.Status.Detector;
using BinanceTrackerDesktop.Window.Extension;
using System.Diagnostics;

namespace BinanceTrackerDesktop.Entry;

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
            new TrackerController(trackerFormView, new UserStatusDetector(new BinaryUserDataSaveSystem(), UserClient.Wallet).GetStatus());
            trackerFormView.ShowDialog();
        }
    }
}
