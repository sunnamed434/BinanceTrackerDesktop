using BinanceTrackerDesktop.Core.User.Wallet.Results;
using BinanceTrackerDesktop.Core.User.Wallet.Results.Coin;

namespace BinanceTrackerDesktop.Core.User.Wallet
{
    public interface IUserWallet
    {
        Task<IUserWalletResult> GetTotalBalanceAsync();

        Task<IEnumerable<IUserWalletCoinResult>> GetAllBuyedCoinsAsync();

        Task<IUserWalletCoinResult> GetBestCoinAsync();
    }
}