namespace BinanceTrackerDesktop.Core.Wallet.Models
{
    public interface IBinanceUserWalletResult
    {
        decimal Value { get; }
    }

    public interface IBinanceUserWalletCoinResult
    {
        string Asset { get; }

        decimal Price { get; }
    }

    public sealed class BinanceUserWalletResult : IBinanceUserWalletResult
    {
        public decimal Value { get; }



        public BinanceUserWalletResult(decimal value)
        {
            Value = value;
        }
    }

    public sealed class BinanceUserWalletCoinResult : IBinanceUserWalletCoinResult
    {
        public string Asset { get; }

        public decimal Price { get; }



        public BinanceUserWalletCoinResult(string asset, decimal price)
        {
            Asset = asset;
            Price = price;
        }
    }
}
