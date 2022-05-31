using BinanceTrackerDesktop.MVC.Controller;
using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Builder;
using BinanceTrackerDesktop.User.Data.Extension;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.User.Data.Value;
using BinanceTrackerDesktop.User.Status.API;
using BinanceTrackerDesktop.User.Status.Result;
using BinanceTrackerDesktop.Views.Tracker;

namespace BinanceTrackerDesktop.Controllers;

public sealed class TrackerController : Controller<TrackerController>
{
    private readonly ITrackerView view;

    private readonly IUserStatus userStatus;

    private bool isBalancesHiden;



    public TrackerController(ITrackerView view, IUserStatus userStatus) : base(view)
    {
        this.view = view;
        this.userStatus = userStatus ?? throw new ArgumentNullException(nameof(userStatus));

        isBalancesHiden = UserDataValues.BalancesHiden.GetValue();

        RefreshTotalBalance();
    }



    public async void RefreshTotalBalance()
    {
        await refreshBalancesFixedAsync();
    }

    public async void ToggleTextsState()
    {
        isBalancesHiden = !isBalancesHiden;
        if (isBalancesHiden)
        {
            SetTextsHidenState();
        }
        else
        {
            await refreshBalancesFixedAsync();
        }

        BinaryUserDataSaveSystem saveSystem = new BinaryUserDataSaveSystem();
        new UserDataBuilder(saveSystem.Read())
            .AddBalancesStateBasedOnData(isBalancesHiden)
            .Build()
            .WriteUserData(saveSystem);
    }

    public void CloseApplication()
    {
        Application.Exit();
    }

    public void SetTextsHidenState()
    {
        view.TotalBalanceText = BinanceTrackerBalanceTextValues.Hiden;
        view.TotalBalanceLossesText = BinanceTrackerBalanceTextValues.Hiden;
    }

    public void SetTextsInitializingState()
    {
        view.TotalBalanceText = BinanceTrackerBalanceTextValues.Initializing;
        view.TotalBalanceLossesText = BinanceTrackerBalanceTextValues.Initializing;
    }

    private async Task refreshBalancesFixedAsync(bool lockButton = true)
    {
        if (isBalancesHiden == false)
        {
            SetTextsInitializingState();

            if (lockButton)
            {
                await refreshBalancesSyncAsync(() => lockRefreshTotalBalanceButton(), () => unlockRefreshTotalBalanceButton());
            }
            else
            {
                await refreshBalancesSyncAsync();
            }
        }
    }

    private void lockRefreshTotalBalanceButton()
    {
        view.RefreshTotalBalanceButtonEnableState = true;
    }

    private void unlockRefreshTotalBalanceButton()
    {
        view.RefreshTotalBalanceButtonEnableState = false;
    }

    private async Task refreshBalancesSyncAsync(Action onStartCallback = null, Action onCompleteCallback = null)
    {
        onStartCallback?.Invoke();

        await refreshBalanceAsync();
        await refreshBalanceLossesAsync();

        onCompleteCallback?.Invoke();
    }

    private async Task refreshBalanceAsync()
    {
        IUserStatusResult totalBalanceStatusResult = await userStatus.CalculateUserTotalBalanceAsync();
        view.TotalBalanceText = userStatus.Format((decimal)totalBalanceStatusResult.Value);
    }

    private async Task refreshBalanceLossesAsync()
    {
        IUserStatusResult balanceLossesStatusResult = await userStatus.CalculateUserBalanceLossesAsync();
        IUserStatusResult totalBalanceStatusResult = await userStatus.CalculateUserTotalBalanceAsync();

        UserData data = new BinaryUserDataSaveSystem().Read();
        string formattedLosses = userStatus.Format((decimal)balanceLossesStatusResult.Value);
        Color balanceLossesColor = getColorFromBalanceLosses((decimal)totalBalanceStatusResult.Value, data.BestBalance);

        view.TotalBalanceLossesText = formattedLosses;
        view.TotalBalanceLossesTextColor = balanceLossesColor;
    }

    private Color getColorFromBalanceLosses(decimal? totalBalance, decimal? bestBalance)
    {
        if (bestBalance == null)
        {
            return Color.Gray;
        }
        else if (bestBalance > totalBalance)
        {
            return Color.Red;
        }
        else if (bestBalance < totalBalance)
        {
            return Color.Green;
        }
        else
        {
            return Color.Gray;
        }
    }



    protected override TrackerController InitializeController()
    {
        return this;
    }
}

public sealed class BinanceTrackerBalanceTextValues
{
    public const string Initializing = "-----";

    public const string Hiden = "*****";
}
