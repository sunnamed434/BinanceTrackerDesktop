using BinanceTrackerDesktop.Core.API;
using BinanceTrackerDesktop.Core.ComponentControl.LabelControl.API;
using BinanceTrackerDesktop.Core.Components.ButtonControl.API;
using BinanceTrackerDesktop.Core.Formatters.API;
using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data.API;
using BinanceTrackerDesktop.Core.User.Data.Extension;
using System;
using System.Drawing;
using System.Threading.Tasks;
using static BinanceTrackerDesktop.Core.Formatters.API.UserBalanceLosesFormatter;

namespace BinanceTrackerDesktop.Core.Forms.Tracker.UI.Balance
{
    public class BinanceTrackerUserBalanceControlUI
    {
        private ISafelyComponentControl formSafelyCloseControl;

        private readonly IBinanceUserStatus userStatus;

        private readonly ButtonComponentControl[] formButtonControls;

        private readonly LabelComponentControl[] formTextControls;

        private bool isBalancesHiden;



        public BinanceTrackerUserBalanceControlUI(ISafelyComponentControl formSafelyCloseControl, IBinanceUserStatus userStatus, ButtonComponentControl[] formButtonControls, LabelComponentControl[] formTextControls)
        {
            if (formSafelyCloseControl == null)
                throw new ArgumentNullException(nameof(formSafelyCloseControl));

            if (userStatus == null)
                throw new ArgumentNullException(nameof(userStatus));

            if (formButtonControls == null)
                throw new ArgumentNullException(nameof(formButtonControls));

            if (formButtonControls.Length < 0)
                throw new InvalidOperationException();

            if (formTextControls == null)
                throw new ArgumentNullException(nameof(formButtonControls));

            if (formTextControls.Length < 0)
                throw new InvalidOperationException();

            this.formSafelyCloseControl = formSafelyCloseControl;
            this.userStatus = userStatus;
            this.formButtonControls = formButtonControls;
            this.formTextControls = formTextControls;

            formTextControls[0].SetDefaultTextColor(Color.Black);
            formTextControls[1].SetDefaultTextColor(Color.Gray);

            setTextsInitializing();
            initializeAsync();

            this.formSafelyCloseControl.RegisterListener(onCloseCallbackAsync);
            this.formButtonControls[0].EventsContainer.ClickEventListener.OnTriggerEventHandler += onRefreshTotalBalanceButtonClicked;
            this.formTextControls[0].EventsContainer.ClickEventListener.OnTriggerEventHandler += onTextClicked;
            this.formTextControls[1].EventsContainer.ClickEventListener.OnTriggerEventHandler += onTextClicked;
        }



        private async void initializeAsync()
        {
            UserData data = new BinaryUserDataSaveReadSystem().Read();
            isBalancesHiden = data.BalancesHiden;

            if (isBalancesHiden)
            {
                setTextsHiden();
            }
            else
            {
                await refreshBalancesFixedAsync();
            }

            await Task.CompletedTask;
        }

        private async Task refreshBalancesFixedAsync(bool lockButton = true)
        {
            if (isBalancesHiden == false)
            {
                setTextsInitializing();

                if (lockButton)
                {
                    await refreshBalancesSyncAsync(() => formButtonControls[0].Lock(), () => formButtonControls[0].Unlock());
                }
                else
                {
                    await refreshBalancesSyncAsync();
                }
            }

            await Task.CompletedTask;
        }

        private async Task refreshBalanceAsync()
        {
            IBinanceUserStatusResult totalBalanceResult = await userStatus.CalculateUserTotalBalanceAsync();
            formTextControls[0].SetText(userStatus.Format(totalBalanceResult.Value));

            await Task.CompletedTask;
        }

        private async Task refreshBalanceLossesAsync()
        {
            UserData data = new BinaryUserDataSaveReadSystem().Read();
            IBinanceUserStatusResult balanceTotalResult = await userStatus.CalculateUserTotalBalanceAsync();
            IBinanceUserStatusResult balanceLossesResult = await userStatus.CalculateUserBalanceLossesAsync();

            formTextControls[1].SetTextAndColor(userStatus.Format(balanceLossesResult.Value), getColorFromBalanceLosses(new BinanceUserBalanceLossesOptions(balanceTotalResult.Value, data.BestBalance)));

            await Task.CompletedTask;
        }

        private async Task refreshBalancesSyncAsync(Action onStartedCallback = null, Action onCompletedCallback = null)
        {
            onStartedCallback?.Invoke();

            await refreshBalanceAsync();
            await refreshBalanceLossesAsync();

            onCompletedCallback?.Invoke();

            await Task.CompletedTask;
        }

        private void setTextsHiden()
        {
            for (int i = 0; i < formTextControls.Length; i++)
            {
                formTextControls[i].SetText(BinanceTrackerBalanceTextValues.Hide);
                formTextControls[i].SetTextColor(Color.Black);
            }
        }

        private void setTextsInitializing()
        {
            for (int i = 0; i < formTextControls.Length; i++)
            {
                formTextControls[i].SetTextAndColor(BinanceTrackerBalanceTextValues.Initialize, formTextControls[i].GetDefaultTextColor());
            }
        }

        private Color getColorFromBalanceLosses(BinanceUserBalanceLossesOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return new UserBalanceLosesFormatter().Format(options);
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

            UserData userData = new BinaryUserDataSaveReadSystem().Read();
            userData.BalancesHiden = isBalancesHiden;
            userData.SaveUserData();
        }

        private async Task onCloseCallbackAsync()
        {
            formButtonControls[0].EventsContainer.ClickEventListener.OnTriggerEventHandler -= onRefreshTotalBalanceButtonClicked;
            formTextControls[0].EventsContainer.ClickEventListener.OnTriggerEventHandler -= onTextClicked;
            formTextControls[1].EventsContainer.ClickEventListener.OnTriggerEventHandler -= onTextClicked;

            await Task.CompletedTask;
        }
    }

    public class BinanceTrackerBalanceTextValues
    {
        public const string Initialize = "-----";

        public const string Hide = "*****";
    }
}
