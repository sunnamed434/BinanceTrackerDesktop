using BinanceTrackerDesktop.Core.Currencies;
using BinanceTrackerDesktop.Core.Formatter;
using BinanceTrackerDesktop.Core.Startup;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Wallet;
using ConsoleBinanceTracker.Core.Wallet.API;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Forms
{
    public partial class BinanceTrackerForm : Form
    {
        private BinanceStartup startup;



        public BinanceTrackerForm()
        {
            InitializeComponent();

            base.Activated += onFormActivated;
            base.FormClosing += onFormClosing;
        }

        private async void onFormActivated(object sender, EventArgs e)
        {
            base.Activated -= onFormActivated;

            startup = new BinanceStartup(await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData);

            BinanceUserWalletResult result = await startup.Wallet.GetTotalBalanceAsync();
            UserTotalBalanceText.Text = BinanceUserWalletStringFormatter.Format(result);
        }

        private async void onRefreshTotalBalanceButtonClick(object sender, EventArgs e)
        {
            BinanceUserWalletResult result = await startup.Wallet.GetTotalBalanceAsync();
            UserTotalBalanceText.Text = BinanceUserWalletStringFormatter.Format(result);
        }

        private async void onFormClosing(object sender, FormClosingEventArgs e)
        {
            base.FormClosing -= onFormClosing;

            BinanceUserWalletResult wallet = await startup.Wallet.GetTotalBalanceAsync();

            BinanceUserData userData = await new BinanceUserDataReader().ReadDataAsync() as BinanceUserData;
            userData.Balance = wallet.Value;
            await new BinanceUserDataWriter().WriteDataAsync(userData);
        }
    }
}
