using BinanceTrackerDesktop.Core.User.Data.FileData;
using System.Runtime.Serialization.Formatters.Binary;

namespace BinanceTrackerDesktop.Core.User.Data.Save.Binary
{
    public sealed class BinaryUserDataSaveSystem : IUserDataSaveSystem
    {
        private readonly BinaryFormatter formatter;



        public BinaryUserDataSaveSystem()
        {
            formatter = new BinaryFormatter();
        }



        public void Write(UserData data)
        {
            if (data != null)
            {
                using (FileStream fileStream = File.Create(UserDataFile.FullPath))
                {
                    formatter.Serialize(fileStream, data);
                }
            }
        }

        public UserData Read()
        {
            if (!File.Exists(UserDataFile.FullPath))
                return null;

            using (FileStream fileStream = File.Open(UserDataFile.FullPath, FileMode.Open))
            {
                return (UserData)formatter.Deserialize(fileStream);
            }
        }
    }
}
