using Binance.Net.Clients;
using Binance.Net.Objects.Models.Spot;
using BinanceTrackerDesktop.Core.Calculator;
using BinanceTrackerDesktop.Core.Calculator.Extension;
using BinanceTrackerDesktop.Core.Formatters.Currency.Crypto;
using BinanceTrackerDesktop.Core.Formatters.Utility;
using BinanceTrackerDesktop.Core.User.Wallet.Models;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Core.User.Wallet
{
    public sealed class UserWallet
    {
        private readonly BinanceClient client;



        internal UserWallet()
        {
            client = new BinanceClient();
        }



        public async Task<UserWalletResult> GetTotalBalanceAsync()
        {
            IEnumerable<UserWalletCoinResult> buyedCoins = await GetAllBuyedCoinsAsync();

            decimal result = decimal.Zero;
            foreach (UserWalletCoinResult buyedCoin in buyedCoins)
            {
                result += buyedCoin.Price;
            }

            return new UserWalletResult(result);
        }

        public async Task<IEnumerable<UserWalletCoinResult>> GetAllBuyedCoinsAsync()
        {
            WebCallResult<IEnumerable<BinanceUserAsset>> coins = await client.SpotApi.Account.GetUserAssetsAsync();

            const string NotExsistableCoin = "Sologenic";
            List<UserWalletCoinResult> result = new List<UserWalletCoinResult>();
            foreach (BinanceUserAsset coin in coins.Data.Where(c => c.Available.ValueFitsToCalculation()))
            {
                if (coin.Name == NotExsistableCoin)
                {
                    continue;
                }

                UserWalletCoinResult coinResult = await calculateAndFormatCoinPriceAsync(coin);
                result.Add(coinResult);
            }

            return result;
        }

        public async Task<UserWalletCoinResult> GetBestCoinAsync()
        {
            IEnumerable<UserWalletCoinResult> coins = await GetAllBuyedCoinsAsync();

            return coins.OrderByDescending(c => c.Price).First();
        }



        private async Task<UserWalletCoinResult> calculateAndFormatCoinPriceAsync(BinanceUserAsset coin)
        {
            string formattedCryptocurrency = FormatterUtility<BasedOnUserDataCryptocurrencyFormatter>.Format(coin.Asset).ToString();

            WebCallResult<BinancePrice> marketPriceResult = await client.SpotApi.ExchangeData.GetPriceAsync(formattedCryptocurrency);

            return new UserWalletCoinResult(formattedCryptocurrency, BinanceCoinCalculator.GetPriceOf(new BinanceCoinOptions(marketPriceResult.Data.Price, coin.Available)));
        }
    }
}
