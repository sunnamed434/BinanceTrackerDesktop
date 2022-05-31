using BinanceTrackerDesktop.User.Wallet.Results;
using BinanceTrackerDesktop.User.Wallet.Results.Coin;

namespace BinanceTrackerDesktop.User.Wallet;

public interface IUserWallet
{
    Task<IUserWalletResult> GetTotalBalanceAsync();

    Task<IEnumerable<IUserWalletCoinResult>> GetAllBuyedCoinsAsync();

    Task<IUserWalletCoinResult> GetBestCoinAsync();
}