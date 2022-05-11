using BinanceTrackerDesktop.Core.ComponentControl.LabelControl;
using BinanceTrackerDesktop.Core.Components.Await.Awaitable.Component;
using BinanceTrackerDesktop.Core.Components.ButtonControl;
using BinanceTrackerDesktop.Core.Components.ButtonControl.Extension;
using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Builder;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using BinanceTrackerDesktop.Core.User.Data.Save.Binary;
using BinanceTrackerDesktop.Core.User.Status.Result;

namespace BinanceTrackerDesktop.Core.Forms.Tracker.UI.Balance
{
    public class BinanceTrackerUserBalanceControlUI : IAwaitableSingletonObject, IAwaitableComponentExecute
    {
        private readonly IUserStatus userStatus;

        private readonly ButtonComponentControl[] formButtonControls;

        private readonly LabelComponentControl[] formTextControls;

        private bool isBalancesHiden;

        private static BinanceTrackerUserBalanceControlUI instance;



        public BinanceTrackerUserBalanceControlUI(IUserStatus userStatus, ButtonComponentControl[] formButtonControls, LabelComponentControl[] formTextControls)
        {
            if (userStatus == null)
                throw new ArgumentNullException(nameof(userStatus));

            if (formButtonControls == null)
                throw new ArgumentNullException(nameof(formButtonControls));

            if (formButtonControls.Any() == false)
                throw new InvalidOperationException();

            if (formTextControls == null)
                throw new ArgumentNullException(nameof(formButtonControls));

            if (formTextControls.Any() == false)
                throw new InvalidOperationException();

            instance = this;

            this.userStatus = userStatus;
            this.formButtonControls = formButtonControls;
            this.formTextControls = formTextControls;

            setTextsInitializing();
            initializeAsync();

            this.formButtonControls[0].EventsContainer.ClickEventListener.OnTriggerEventHandler += onRefreshTotalBalanceButtonClicked;
            this.formTextControls[0].EventsContainer.ClickEventListener.OnTriggerEventHandler += onTextClicked;
            this.formTextControls[1].EventsContainer.ClickEventListener.OnTriggerEventHandler += onTextClicked;
        }



        object IAwaitableSingletonObject.Instance => instance;



        void IAwaitableComponentExecute.OnExecute()
        {
            formButtonControls[0].EventsContainer.ClickEventListener.OnTriggerEventHandler -= onRefreshTotalBalanceButtonClicked;
            formTextControls[0].EventsContainer.ClickEventListener.OnTriggerEventHandler -= onTextClicked;
            formTextControls[1].EventsContainer.ClickEventListener.OnTriggerEventHandler -= onTextClicked;
        }



        private async void initializeAsync()
        {
            UserData data = new BinaryUserDataSaveSystem().Read();
            isBalancesHiden = data.IsBalancesHiden ?? default(bool);

            if (isBalancesHiden)
            {
                setTextsHiden();
            }
            else
            {
                await refreshBalancesFixedAsync();
            }
        }

        private async Task refreshBalancesFixedAsync(bool lockButton = true)
        {
            if (isBalancesHiden == false)
            {
                setTextsInitializing();

                if (lockButton)
                {
                    await refreshBalancesSyncAsync(() => formButtonControls[0].LockButton(), () => formButtonControls[0].UnlockButton());
                }
                else
                {
                    await refreshBalancesSyncAsync();
                }
            }
        }

        private async Task refreshBalanceAsync()
        {
            IUserStatusResult totalBalanceStatusResult = await userStatus.CalculateUserTotalBalanceAsync();
            formTextControls[0].SetText(userStatus.Format((decimal)totalBalanceStatusResult.Value));
        }

        private async Task refreshBalanceLossesAsync()
        {
            IUserStatusResult balanceLossesStatusResult = await userStatus.CalculateUserBalanceLossesAsync();
            IUserStatusResult totalBalanceStatusResult = await userStatus.CalculateUserTotalBalanceAsync();

            UserData data = new BinaryUserDataSaveSystem().Read();
            string formattedLosses = userStatus.Format((decimal)balanceLossesStatusResult.Value);
            Color balanceLossesColor = getColorFromBalanceLosses((decimal)totalBalanceStatusResult.Value, data.BestBalance);

            formTextControls[1].SetText(formattedLosses, balanceLossesColor);
        }

        private async Task refreshBalancesSyncAsync(Action onStartCallback = null, Action onCompleteCallback = null)
        {
            onStartCallback?.Invoke();

            await refreshBalanceAsync();
            await refreshBalanceLossesAsync();

            onCompleteCallback?.Invoke();
        }

        private void setTextsHiden()
        {
            for (int i = 0; i < formTextControls.Length; i++)
            {
                formTextControls[i].SetText(BinanceTrackerBalanceTextValues.Hiden);
            }
        }

        private void setTextsInitializing()
        {
            for (int i = 0; i < formTextControls.Length; i++)
            {
                formTextControls[i].SetText(BinanceTrackerBalanceTextValues.Initializing, formTextControls[i].GetDefaultTextColor());
            }
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



        private async void onRefreshTotalBalanceButtonClicked(EventArgs e)
        {
            await refreshBalancesFixedAsync();
        }

        private async void onTextClicked(EventArgs e)
        {
            isBalancesHiden = !isBalancesHiden;
            if (isBalancesHiden)
            {
                setTextsHiden();
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
    }

    public sealed class BinanceTrackerBalanceTextValues
    {
        public const string Initializing = "-----";

        public const string Hiden = "*****";
    }
}