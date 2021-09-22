using BinanceTrackerDesktop.Core.User.Data.API;
using System;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Core.User.Data.Extension
{
    public static class UserDataExtension
    {
        public static void SaveUserData(this UserData source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            new BinaryUserDataSaveReadSystem().Save(source);
        }
    }
}
