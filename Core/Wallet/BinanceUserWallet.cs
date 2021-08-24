using Binance.Net;
using Binance.Net.Objects.Spot.SpotData;
using BinanceTrackerDesktop.Core.Calculator;
using BinanceTrackerDesktop.Core.Calculator.API;
using BinanceTrackerDesktop.Core.Calculator.Extension;
using BinanceTrackerDesktop.Core.Currencies;
using ConsoleBinanceTracker.Core.Wallet.API;
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



        internal BinanceUserWallet(BinanceClient client)
        {
            if (client == null)
                throw new ArgumentNullException(nameof(client));

            this.client = client;
        }



        public async Task<BinanceUserWalletResult> GetTotalBalanceAsync()
        {
            IEnumerable<BinanceUserWalletCoinResult> buyedCoins = await GetBuyedCoinsAsync();

            decimal result = 0;
            foreach (BinanceUserWalletCoinResult coin in buyedCoins)
                result += coin.Price;

            return new BinanceUserWalletResult(result);
        }

        public async Task<IEnumerable<BinanceUserWalletCoinResult>> GetBuyedCoinsAsync()
        {
            WebCallResult<BinanceAccountInfo> accountInfo = await client.General.GetAccountInfoAsync();

            List<BinanceUserWalletCoinResult> result = new List<BinanceUserWalletCoinResult>();
            foreach (BinanceBalance balance in accountInfo.Data.Balances.Where(b => b.Total.ValueFitsToCalculation()))
                result.Add(new BinanceUserWalletCoinResult(balance.Asset + CurrencyName.EUR, BinanceCoinCalculator.GetPrice(new BinanceCoinOptions(client.Spot.Market.GetPriceAsync(balance.Asset + CurrencyName.EUR).Result.Data.Price, balance.Total))));

            return result;
        }
    }
}
