using BinanceTrackerDesktop.Core.Control.FormButton.API;
using BinanceTrackerDesktop.Core.Control.FormText.API;
using BinanceTrackerDesktop.Core.Formatters.API;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.API;
using BinanceTrackerDesktop.Forms.Tracker.Startup.API;
using System;
using System.Drawing;
using System.Threading.Tasks;
using static BinanceTrackerDesktop.Core.Formatters.API.BinanceUserBalanceLosesColorFormatter;

namespace BinanceTrackerDesktop.Forms.Tracker.UI
{
    public class BinanceTrackerUserBalanceUIControl
    {
        private IFormSafelyComponentControl formSafelyCloseControl;

        private readonly IBinanceUserStatus userStatus;

        private readonly IFormButtonControl[] formButtonControls;

        private readonly IFormTextControl[] formTextControls;

        private bool isBalancesHiden;



        private const string Initializing = "-----";

        private const string Hiden = "*****";



        public BinanceTrackerUserBalanceUIControl(IFormSafelyComponentControl formSafelyCloseControl, IBinanceUserStatus userStatus, IFormButtonControl[] formButtonControls, IFormTextControl[] formTextControls)
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
            async void initializeAsync()
            {
                BinanceUserData data = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
                isBalancesHiden = data.BalancesHiden;

                if (isBalancesHiden)
                {
                    setTextsHiden();
                }
                else
                {
                    await refreshBalancesFixedAsync(false);
                }
            }

            this.formSafelyCloseControl.RegisterListener(onCloseCallbackAsync);
            this.formButtonControls[0].ClickEvent.OnTriggerEventHandler += onRefreshTotalBalanceButtonClicked;
            this.formTextControls[0].ClickEvent.OnTriggerEventHandler += onTextClicked;
            this.formTextControls[1].ClickEvent.OnTriggerEventHandler += onTextClicked;
        }



        private async Task refreshBalancesFixedAsync(bool lockButton = true)
        {
            if (isBalancesHiden == false)
            {
                setTextsInitializing();

                if (lockButton)
                {
                    await refreshBalancesSyncAsync(() => formButtonControls[0].Button.Enabled = false, () => formButtonControls[0].Button.Enabled = true);
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
            BinanceUserData data = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            IBinanceUserStatusResult balanceTotalResult = await userStatus.CalculateUserTotalBalanceAsync();
            IBinanceUserStatusResult balanceLossesResult = await userStatus.CalculateUserBalanceLossesAsync();

            formTextControls[1].SetTextSync(userStatus.Format(balanceLossesResult.Value), getColorFromBalanceLosses(new BinanceUserBalanceLossesOptions(balanceTotalResult.Value, data.BestBalance)));

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
                formTextControls[i].SetTextSync(Hiden, Color.Black);
            }
        }

        private void setTextsInitializing()
        {
            for (int i = 0; i < formTextControls.Length; i++)
            {
                formTextControls[i].SetTextSync(Initializing, formTextControls[i].GetDefaultTextColor());
            }
        }

        private Color getColorFromBalanceLosses(BinanceUserBalanceLossesOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return new BinanceUserBalanceLosesColorFormatter().Format(options);
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
                await refreshBalancesFixedAsync(false);
            }

            BinanceUserData userData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            userData.BalancesHiden = isBalancesHiden;
            await new BinanceUserDataWriter().WriteDataAsync(userData);
        }

        private async Task onCloseCallbackAsync()
        {
            formButtonControls[0].ClickEvent.OnTriggerEventHandler -= onRefreshTotalBalanceButtonClicked;
            formTextControls[0].ClickEvent.OnTriggerEventHandler -= onTextClicked;
            formTextControls[1].ClickEvent.OnTriggerEventHandler -= onTextClicked;

            await Task.CompletedTask;
        }
    }
}
