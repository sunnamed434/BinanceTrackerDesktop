using BinanceTrackerDesktop.Core.Controls.FormButton.API;
using BinanceTrackerDesktop.Core.Controls.FormText.API;
using BinanceTrackerDesktop.Core.Formatters.API;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Forms.API;
using BinanceTrackerDesktop.Forms.Tracker.Startup.API;
using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using static BinanceTrackerDesktop.Core.Formatters.API.BinanceUserBalanceLosesColorFormatter;

namespace BinanceTrackerDesktop.Forms.Tracker.UI
{
    public class BinanceTrackerUserBalanceUIControl
    {
        private readonly IFormControl formControl;

        private readonly IBinanceUserStatus userStatus;

        private readonly IFormButtonControl[] formButtonControls;

        private readonly IFormTextControl[] formTextControls;

        private bool isBalancesHiden;



        private const string Initializing = "--------";

        private const string Hiden = "********";



        public BinanceTrackerUserBalanceUIControl(IFormControl formControl, IBinanceUserStatus userStatus, IFormButtonControl[] formButtonControls, IFormTextControl[] formTextControls)
        {
            if (formControl == null)
                throw new ArgumentNullException(nameof(formControl));

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

            this.formControl = formControl;
            this.userStatus = userStatus;
            this.formButtonControls = formButtonControls;
            this.formTextControls = formTextControls;

            initializeAsync();
            async void initializeAsync()
            {
                for (int i = 0; i < formTextControls.Length; i++)
                {
                    formTextControls[i].SetText(Initializing);
                }

                BinanceUserData data = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
                isBalancesHiden = data.BalancesHiden;

                if (isBalancesHiden)
                {
                    for (int i = 0; i < formTextControls.Length; i++)
                    {
                        formTextControls[i].SetText(Hiden);
                    }
                }
                else
                {
                    await refreshBalancesFixed(false);
                }
            }

            formControl.FormClosing += onFormClosing;
            formButtonControls[0].ClickEvent.OnTriggerEventHandler += onRefreshTotalBalanceButtonClicked;
            formTextControls[0].ClickEvent.OnTriggerEventHandler += onTextClicked;
            formTextControls[1].ClickEvent.OnTriggerEventHandler += onTextClicked;
        }



        private async Task refreshBalancesFixed(bool disableButton = true)
        {
            if (formButtonControls[0].Button.Enabled)
            {
                if (disableButton)
                {
                    await refreshBalancesSync(() => formButtonControls[0].Button.Enabled = false, () => formButtonControls[0].Button.Enabled = true);
                }
                else
                {
                    await refreshBalancesSync();
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
            IBinanceUserStatusResult balanceLossesResult = await userStatus.CalculateUserBalanceLossesAsync();

            formTextControls[1].SetTextSync(userStatus.Format(balanceLossesResult.Value), getColorFromBalanceLosses(new BinanceUserBalanceLossesOptions(balanceLossesResult.Value, data)));

            await Task.CompletedTask;
        }


        private async Task refreshBalancesSync(Action onStartedCallback = null, Action onCompletedCallback = null)
        {
            onStartedCallback?.Invoke();

            await refreshBalanceAsync();
            await refreshBalanceLossesAsync();

            onCompletedCallback?.Invoke();

            await Task.CompletedTask;
        }


        private Color getColorFromBalanceLosses(BinanceUserBalanceLossesOptions options)
        {
            if (options == null)
                throw new ArgumentNullException(nameof(options));

            return new BinanceUserBalanceLosesColorFormatter().Format(options);
        }



        private async void onRefreshTotalBalanceButtonClicked(object sender, EventArgs e)
        {
            await refreshBalancesFixed();
        }

        private async void onTextClicked(object sender, EventArgs e)
        {
            isBalancesHiden = !isBalancesHiden;

            if (isBalancesHiden)
            {
                formTextControls[0].SetText(Hiden);
                formTextControls[1].SetText(Hiden);
            }
            else
            {
                await refreshBalancesFixed(false);
            }

            BinanceUserData userData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            userData.BalancesHiden = isBalancesHiden;
            await new BinanceUserDataWriter().WriteDataAsync(userData);
        }

        private void onFormClosing(object sender, FormClosingEventArgs e)
        {
            formControl.FormClosing -= onFormClosing;
            formButtonControls[0].ClickEvent.OnTriggerEventHandler -= onRefreshTotalBalanceButtonClicked;
            formTextControls[0].ClickEvent.OnTriggerEventHandler -= onTextClicked;
            formTextControls[1].ClickEvent.OnTriggerEventHandler -= onTextClicked;
        }
    }
}
