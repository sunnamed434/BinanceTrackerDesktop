using Binance.Net;
using Binance.Net.Objects;
using BinanceTrackerDesktop.Core.User.Control;
using BinanceTrackerDesktop.Core.User.Data.API;
using BinanceTrackerDesktop.Core.Wallet;
using CryptoExchange.Net.Authentication;
using System;

namespace BinanceTrackerDesktop.Core.User.Client
{
    public class UserClient
    {
        public readonly IUserDataSaveSystem SaveDataSystem;

        public readonly BinanceUserWallet Wallet;

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
            });

            BinanceSocketClient.SetDefaultOptions(new BinanceSocketClientOptions
            {
                ApiCredentials = new ApiCredentials(data.Key, data.Secret),
            });

            Wallet = new BinanceUserWallet();
            Status = new UserStatusDetector(SaveDataSystem.Read(), Wallet).GetStatus();
        }
    }
}
