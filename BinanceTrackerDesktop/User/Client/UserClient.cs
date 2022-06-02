using Binance.Net.Clients;
using Binance.Net.Enums;
using Binance.Net.Objects;
using BinanceTrackerDesktop.User.Data;
using BinanceTrackerDesktop.User.Data.Save;
using BinanceTrackerDesktop.User.Data.Save.Binary;
using BinanceTrackerDesktop.User.Status.API;
using BinanceTrackerDesktop.User.Status.Detector;
using BinanceTrackerDesktop.User.Wallet;
using CryptoExchange.Net.Authentication;

namespace BinanceTrackerDesktop.User.Client;

public sealed class UserClient
{
    public static readonly IUserDataSaveSystem SaveDataSystem;

    public static readonly UserWallet Wallet;

    public static readonly IUserStatus Status;

    public static readonly BinanceClient BinanceClient;



    static UserClient()
    {
        SaveDataSystem = new BinaryUserDataSaveSystem();
        UserData data = SaveDataSystem.Read();

        if (data == null)
        {
            throw new InvalidOperationException();
        }

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
        Status = new UserStatusDetector(SaveDataSystem, Wallet).GetStatus();
        BinanceClient = new BinanceClient();
    }
}
