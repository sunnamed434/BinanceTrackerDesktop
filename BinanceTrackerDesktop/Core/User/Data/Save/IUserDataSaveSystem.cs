namespace BinanceTrackerDesktop.Core.User.Data.Save
{
    public interface IUserDataSaveSystem
    {
        void Write(UserData data);

        UserData Read();
    }
}
