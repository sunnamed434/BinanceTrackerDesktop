using BinanceTrackerDesktop.Core.User.Data.Save.Binary;

namespace BinanceTrackerDesktop.Core.User.Data.Extension
{
    public static class UserDataSaveExtension
    {
        public static void SaveUserData(this UserData source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            new BinaryUserDataSaveSystem().Write(source);
        }
    }
}
