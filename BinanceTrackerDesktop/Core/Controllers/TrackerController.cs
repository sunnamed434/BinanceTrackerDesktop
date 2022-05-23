using BinanceTrackerDesktop.Core.MVC.Controller;
using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Builder;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Status.Result;
using BinanceTrackerDesktop.Core.Views.Tracker;

namespace BinanceTrackerDesktop.Core.Controllers
{
    public sealed class TrackerController : Controller<TrackerController>
    {
        private readonly ITrackerView view;

        private readonly IUserStatus userStatus;

        private bool isBalancesHiden;



        public TrackerController(ITrackerView view, IUserStatus userStatus) : base(view)
        {
            this.view = view;
            this.userStatus = userStatus ?? throw new ArgumentNullException(nameof(userStatus));
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
            this.view.RefreshTotalBalanceButtonEnableState = true;
        }

        private void unlockRefreshTotalBalanceButton()
        {
            this.view.RefreshTotalBalanceButtonEnableState = false;
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
            this.view.TotalBalanceText = userStatus.Format((decimal)totalBalanceStatusResult.Value);
        }

        private async Task refreshBalanceLossesAsync()
        {
            IUserStatusResult balanceLossesStatusResult = await userStatus.CalculateUserBalanceLossesAsync();
            IUserStatusResult totalBalanceStatusResult = await userStatus.CalculateUserTotalBalanceAsync();

            UserData data = new BinaryUserDataSaveSystem().Read();
            string formattedLosses = userStatus.Format((decimal)balanceLossesStatusResult.Value);
            Color balanceLossesColor = getColorFromBalanceLosses((decimal)totalBalanceStatusResult.Value, data.BestBalance);

            this.view.TotalBalanceLossesText = formattedLosses;
            this.view.TotalBalanceLossesTextColor = balanceLossesColor;
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
}
