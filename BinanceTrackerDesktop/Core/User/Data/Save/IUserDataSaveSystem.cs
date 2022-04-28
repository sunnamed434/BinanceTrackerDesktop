using BinanceTrackerDesktop.Core.DirectoryFiles.Format;
using BinanceTrackerDesktop.Core.DirectoryFiles.Paths;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using static BinanceTrackerDesktop.Core.DirectoryFiles.Control.DirectoryFilesControl;

namespace BinanceTrackerDesktop.Core.User.Data.Save
{
    public interface IUserDataSaveSystem
    {
        void Save(UserData data);

        UserData Read();
    }

    public sealed class BinaryUserDataSaveSystem : IUserDataSaveSystem
    {
        private readonly BinaryFormatter formatter;



        public BinaryUserDataSaveSystem()
        {
            formatter = new BinaryFormatter();
        }



        public void Save(UserData data)
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

    public sealed class UserDataFile
    {
        public static readonly string FullPath = Path.Combine(ApplicationDirectoryPaths.User, new StringBuilder()
            .Append(RegisteredData.UserFile)
            .Append(FilesFormatExtensions.DAT)
            .ToString());
    }
}
