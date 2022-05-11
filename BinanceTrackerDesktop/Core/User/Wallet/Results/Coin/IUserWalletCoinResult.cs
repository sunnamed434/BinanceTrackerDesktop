namespace BinanceTrackerDesktop.Core.User.Wallet.Results.Coin
{
    public interface IUserWalletCoinResult
    {
        string Asset { get; }

        decimal Price { get; }
    }
}
