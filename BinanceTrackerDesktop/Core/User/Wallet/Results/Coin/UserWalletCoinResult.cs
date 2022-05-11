namespace BinanceTrackerDesktop.Core.User.Wallet.Results.Coin
{
    public sealed class UserWalletCoinResult : IUserWalletCoinResult
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
