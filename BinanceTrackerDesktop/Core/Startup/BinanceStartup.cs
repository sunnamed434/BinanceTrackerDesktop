using Binance.Net;
using Binance.Net.Objects;
using BinanceTrackerDesktop.Core.User.Data.API;
using BinanceTrackerDesktop.Core.Wallet;
using CryptoExchange.Net.Authentication;
using System;

namespace BinanceTrackerDesktop.Core.Startup
{
    public class BinanceStartup
    {
        public readonly BinanceUserWallet Wallet;



        public BinanceStartup(UserData data)
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

            Wallet = new BinanceUserWallet();
        }
    }
}
