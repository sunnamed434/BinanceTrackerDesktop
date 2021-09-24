using Binance.Net;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.WalletData;
using BinanceTrackerDesktop.Core.Calculator;
using BinanceTrackerDesktop.Core.Calculator.API;
using BinanceTrackerDesktop.Core.Calculator.Extension;
using BinanceTrackerDesktop.Core.Formatters.API;
using BinanceTrackerDesktop.Core.Wallet.API;
using CryptoExchange.Net.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Core.Wallet
{
    public class BinanceUserWallet
    {
        private readonly BinanceClient client;



        internal BinanceUserWallet()
        {
            client = new BinanceClient();
        }



        public async Task<BinanceUserWalletResult> GetTotalBalanceAsync()
        {
            IEnumerable<BinanceUserWalletCoinResult> buyedCoins = await GetAllBuyedCoinsAsync();

            decimal result = decimal.Zero;

            foreach (BinanceUserWalletCoinResult coin in buyedCoins)
            {
                result += coin.Price;
            }

            return new BinanceUserWalletResult(result);
        }

        public async Task<IEnumerable<BinanceUserWalletCoinResult>> GetAllBuyedCoinsAsync()
        {
            WebCallResult<IEnumerable<BinanceUserCoin>> coins = await client.General.GetUserCoinsAsync();

            List<BinanceUserWalletCoinResult> result = new List<BinanceUserWalletCoinResult>();
            foreach (BinanceUserCoin coin in coins.Data.Where(b => b.Free.ValueFitsToCalculation()))
            {
                BinanceUserWalletCoinResult coinResult = await calculateAndFormatCoinPriceAsync(coin);
                result.Add(coinResult);
            }

            return result;
        }



        private async Task<BinanceUserWalletCoinResult> calculateAndFormatCoinPriceAsync(BinanceUserCoin coin)
        {
            string formattedCryptocurrency = new CryptocurrencyFormatter().Format(coin.Coin);
            WebCallResult<BinancePrice> marketPriceResult = await client.Spot.Market.GetPriceAsync(formattedCryptocurrency);

            return new BinanceUserWalletCoinResult(formattedCryptocurrency, BinanceCoinCalculator.GetPriceOf(new BinanceCoinOptions(marketPriceResult.Data.Price, coin.Free)));
        }
    }
}
