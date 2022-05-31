namespace BinanceTrackerDesktop.User.Status.Result;

public sealed class UserStatusResult : IUserStatusResult
{
    public object Value { get; }



    public UserStatusResult(object value)
    {
        if (value == null)
        {
            throw new ArgumentNullException(nameof(value));
        }

        Value = value;
    }
}
