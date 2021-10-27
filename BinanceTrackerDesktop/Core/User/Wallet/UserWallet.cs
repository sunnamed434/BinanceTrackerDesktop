using Binance.Net;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.WalletData;
using BinanceTrackerDesktop.Core.Calculator;
using BinanceTrackerDesktop.Core.Calculator.Extension;
using BinanceTrackerDesktop.Core.Calculator.Models;
using BinanceTrackerDesktop.Core.Formatters.Models;
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
            WebCallResult<IEnumerable<BinanceUserCoin>> coins = await client.General.GetUserCoinsAsync();

            List<UserWalletCoinResult> result = new List<UserWalletCoinResult>();
            foreach (BinanceUserCoin coin in coins.Data.Where(c => c.Free.ValueFitsToCalculation()))
            {
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



        private async Task<UserWalletCoinResult> calculateAndFormatCoinPriceAsync(BinanceUserCoin coin)
        {
            string formattedCryptocurrency = FormatterUtility<string, CryptocurrencyFormatter>.Format(coin.Coin).ToString();
            WebCallResult<BinancePrice> marketPriceResult = await client.Spot.Market.GetPriceAsync(formattedCryptocurrency);

            return new UserWalletCoinResult(formattedCryptocurrency, BinanceCoinCalculator.GetPriceOf(new BinanceCoinOptions(marketPriceResult.Data.Price, coin.Free)));
        }
    }
}
