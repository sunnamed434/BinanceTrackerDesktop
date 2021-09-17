using Binance.Net;
using Binance.Net.Objects.Spot.MarketData;
using Binance.Net.Objects.Spot.SpotData;
using BinanceTrackerDesktop.Core.Calculator;
using BinanceTrackerDesktop.Core.Calculator.API;
using BinanceTrackerDesktop.Core.Calculator.Extension;
using BinanceTrackerDesktop.Core.Formatters.API;
using BinanceTrackerDesktop.Core.Wallet.API;
using CryptoExchange.Net.Objects;
using System;
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
            IEnumerable<BinanceUserWalletCoinResult> buyedCoins = await GetBuyedCoinsAsync();

            decimal result = decimal.Zero;
            foreach (BinanceUserWalletCoinResult coin in buyedCoins)
            {
                result += coin.Price;
            }

            return new BinanceUserWalletResult(result);
        }

        public async Task<IEnumerable<BinanceUserWalletCoinResult>> GetBuyedCoinsAsync()
        {
            WebCallResult<BinanceAccountInfo> binanceAccountInfo = await client.General.GetAccountInfoAsync();

            List<BinanceUserWalletCoinResult> result = new List<BinanceUserWalletCoinResult>();
            foreach (BinanceBalance balance in binanceAccountInfo.Data.Balances.Where(b => b.Total.ValueFitsToCalculation()))
            {
                BinanceUserWalletCoinResult coinResult = await calculateAndFormatCoinPriceAsync(balance);
                result.Add(coinResult);
            }

            return result;
        }



        private async Task<BinanceUserWalletCoinResult> calculateAndFormatCoinPriceAsync(BinanceBalance balance)
        {
            string formattedBalance = new BinanceCryptocurrencyStringFormatter().Format(balance.Asset);
            WebCallResult<BinancePrice> marketPriceResult = await client.Spot.Market.GetPriceAsync(formattedBalance);

            return new BinanceUserWalletCoinResult(formattedBalance, BinanceCoinCalculator.GetPriceOf(new BinanceCoinOptions(marketPriceResult.Data.Price, balance.Total)));
        }
    }
}
