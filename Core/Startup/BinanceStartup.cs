using Binance.Net;
using Binance.Net.Objects;
using BinanceTrackerDesktop.Core.UserData.API;
using BinanceTrackerDesktop.Core.Wallet;
using CryptoExchange.Net.Authentication;
using System;

namespace BinanceTrackerDesktop.Core.Startup
{
    public class BinanceStartup
    {
        public readonly BinanceUserWallet Wallet;



        public BinanceStartup(IBinanceUserData userData)
        {
            if (userData == null)
                throw new ArgumentNullException(nameof(userData));

            BinanceClient.SetDefaultOptions(new BinanceClientOptions()
            {
                ApiCredentials = new ApiCredentials(userData.Key, userData.Secret),
            });

            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions()
            {
                ApiCredentials = new ApiCredentials(userData.Key, userData.Secret),
            });

            Wallet = new BinanceUserWallet(new BinanceClient());
        }
    }
}
