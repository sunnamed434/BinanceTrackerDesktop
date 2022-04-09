using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;
using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data;
using BinanceTrackerDesktop.Core.User.Data.Save;
using BinanceTrackerDesktop.Core.User.Status.Extension;
using BinanceTrackerDesktop.Core.User.Wallet;
using CryptoExchange.Net.Authentication;
using System;

namespace BinanceTrackerDesktop.Core.User.Client
{
    public sealed class UserClient
    {
        public readonly IUserDataSaveSystem SaveDataSystem;

        public readonly UserWallet Wallet;

        public readonly IUserStatus Status;



        public UserClient()
        {
            SaveDataSystem = new BinaryUserDataSaveSystem();
            UserData data = SaveDataSystem.Read();

            if (data == null)
                throw new InvalidOperationException();

            BinanceClient.SetDefaultOptions(new BinanceClientOptions
            {
                ApiCredentials = new ApiCredentials(data.Key, data.Secret),
                SpotApiOptions = new BinanceApiClientOptions
                {
                    BaseAddress = BinanceApiAddresses.Default.RestClientAddress,
                    AutoTimestamp = false
                },
                UsdFuturesApiOptions = new BinanceApiClientOptions
                {
                    TradeRulesBehaviour = TradeRulesBehaviour.ThrowError,
                    BaseAddress = BinanceApiAddresses.Default.UsdFuturesRestClientAddress,
                    AutoTimestamp = true
                }
            });

            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions
            {
                ApiCredentials = new ApiCredentials(data.Key, data.Secret),
            });

            Wallet = new UserWallet();
            Status = this.GetUserStatus();
        }
    }
}
