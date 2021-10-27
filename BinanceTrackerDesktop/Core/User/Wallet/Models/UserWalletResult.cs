namespace BinanceTrackerDesktop.Core.User.Wallet.Models
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

    public sealed class UserWalletResult : IBinanceUserWalletResult
    {
        public decimal Value { get; }



        public UserWalletResult(decimal value)
        {
            Value = value;
        }
    }

    public sealed class UserWalletCoinResult : IBinanceUserWalletCoinResult
    {
        public string Asset { get; }

        public decimal Price { get; }



        public UserWalletCoinResult(string asset, decimal price)
        {
            Asset = asset;
            Price = price;
        }
    }
}
