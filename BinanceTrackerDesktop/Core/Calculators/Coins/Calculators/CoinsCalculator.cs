using BinanceTrackerDesktop.Core.Calculator.Options;

namespace BinanceTrackerDesktop.Core.Calculators.Coins.Calculators
{
    public class CoinsCalculator : ICoinsCalculator
    {
        public decimal CalculatePrice(ICoin coin)
        {
            if (coin == null)
            {
                throw new ArgumentNullException(nameof(coin));
            }

            return coin.Amount * coin.Price;
        }

        public decimal CalculatePrice(IEnumerable<ICoin> coins)
        {
            if (coins == null)
            {
                throw new ArgumentNullException(nameof(coins));
            }

            if (coins.Any() == false)
            {
                throw new InvalidOperationException();
            }

            decimal result = decimal.Zero;
            foreach (Coin coin in coins)
            {
                if (coin == null)
                {
                    throw new ArgumentNullException(nameof(coin));
                }

                result += CalculatePrice(coin);
            }

            return result;
        }
    }
}
