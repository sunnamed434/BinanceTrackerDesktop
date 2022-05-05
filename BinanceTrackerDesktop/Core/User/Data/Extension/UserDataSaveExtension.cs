using BinanceTrackerDesktop.Core.User.Data.Save;

namespace BinanceTrackerDesktop.Core.User.Data.Extension
{
    public static class UserDataSaveExtension
    {
        public static void WriteUserData(this UserData source, IUserDataSaveSystem system)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (system == null)
                throw new ArgumentNullException(nameof(system));

            system.Write(source);
        }

        public static UserData WriteUserDataThenRead(this UserData source, IUserDataSaveSystem system)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (system == null)
                throw new ArgumentNullException(nameof(system));

            WriteUserData(source, system);
            return system.Read();
        }
    }
}
