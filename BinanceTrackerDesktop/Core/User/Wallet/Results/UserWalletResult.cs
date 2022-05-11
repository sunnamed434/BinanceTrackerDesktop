namespace BinanceTrackerDesktop.Core.User.Wallet.Results
{
    public sealed class UserWalletResult : IUserWalletResult
    {
        public decimal Value { get; }



        public UserWalletResult(decimal value)
        {
            Value = value;
        }
    }
}
