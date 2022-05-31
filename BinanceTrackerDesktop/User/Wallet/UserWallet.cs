using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using BinanceTrackerDesktop.Calculators;
using BinanceTrackerDesktop.Calculators.Coins;
using BinanceTrackerDesktop.Calculators.Extensions;
using BinanceTrackerDesktop.Formatters.Currency.Crypto;
using BinanceTrackerDesktop.Formatters.Utilities;
using BinanceTrackerDesktop.User.Wallet.Results;
using BinanceTrackerDesktop.User.Wallet.Results.Coin;
using CryptoExchange.Net.Objects;

namespace BinanceTrackerDesktop.User.Wallet;

public sealed class UserWallet : IUserWallet
{
    private readonly BinanceClient client;



    internal UserWallet()
    {
        client = new BinanceClient();
    }



    public async Task<IUserWalletResult> GetTotalBalanceAsync()
    {
        IEnumerable<IUserWalletCoinResult> buyedCoins = await GetAllBuyedCoinsAsync();

        decimal result = decimal.Zero;
        foreach (UserWalletCoinResult buyedCoin in buyedCoins)
        {
            result += buyedCoin.Price;
        }

        return new UserWalletResult(result);
    }

    public async Task<IEnumerable<IUserWalletCoinResult>> GetAllBuyedCoinsAsync()
    {
        WebCallResult<IEnumerable<BinanceUserAsset>> coins = await client.SpotApi.Account.GetUserAssetsAsync();

        const string NotExsistableCoin = "Sologenic";
        List<IUserWalletCoinResult> result = new List<IUserWalletCoinResult>();
        if (coins.Success)
        {
            foreach (BinanceUserAsset coin in coins.Data.Where(c => c.Available.ValueFitsForCalculation()))
            {
                if (coin.Name == NotExsistableCoin)
                {
                    continue;
                }

                IUserWalletCoinResult coinResult = await calculateAndFormatCoinPriceAsync(coin);
                result.Add(coinResult);
            }
        }

        return result;
    }

    public async Task<IUserWalletCoinResult> GetBestCoinAsync()
    {
        IEnumerable<IUserWalletCoinResult> coins = await GetAllBuyedCoinsAsync();

        return coins.OrderByDescending(c => c.Price).First();
    }



    private async Task<IUserWalletCoinResult> calculateAndFormatCoinPriceAsync(BinanceUserAsset coin)
    {
        string formattedCryptocurrency = FormatterUtility<BasedOnUserDataCryptocurrencyFormatter>.Format(coin.Asset).ToString();

        WebCallResult<BinancePrice> marketPriceResult = await client.SpotApi.ExchangeData.GetPriceAsync(formattedCryptocurrency);

        return new UserWalletCoinResult(formattedCryptocurrency, CoinsCalculators.CoinsCalculator.CalculatePrice(new Coin(marketPriceResult.Data.Price, coin.Available)));
    }
}
