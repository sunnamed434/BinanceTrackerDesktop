using Binance.Net;
using Binance.Net.Objects;
using BinanceTrackerDesktop.Core.UserData;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Wallet;
using CryptoExchange.Net.Authentication;
using System;
using System.Windows.Forms;

namespace BinanceTrackerDesktop.Core.Startup
{
    public class BinanceStartup
    {
        public readonly BinanceUserData Data;

        public readonly BinanceUserWallet Wallet;



        public BinanceStartup(BinanceUserData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(data.Key, data.Secret),
            });

            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
            {
                ApiCredentials = new ApiCredentials(data.Key, data.Secret),
            });

            Wallet = new BinanceUserWallet(new BinanceClient());
        }
    }
}
