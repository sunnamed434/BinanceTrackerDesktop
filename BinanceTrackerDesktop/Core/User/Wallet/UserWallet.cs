using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using BinanceTrackerDesktop.Core.Calculator;
using BinanceTrackerDesktop.Core.Calculator.Extensions;
using BinanceTrackerDesktop.Core.Calculator.Options;
using BinanceTrackerDesktop.Core.Formatters.Currency.Crypto;
using BinanceTrackerDesktop.Core.Formatters.Utility;
using BinanceTrackerDesktop.Core.User.Wallet.Results;
using BinanceTrackerDesktop.Core.User.Wallet.Results.Coin;
using CryptoExchange.Net.Objects;

namespace BinanceTrackerDesktop.Core.User.Wallet
{
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

            return new UserWalletCoinResult(formattedCryptocurrency, BinanceCoinCalculator.GetPriceOf(new BinanceCoinOptions(marketPriceResult.Data.Price, coin.Available)));
        }
    }
}
