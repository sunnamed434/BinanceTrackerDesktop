using BinanceTrackerDesktop.User.Data.FileData;
using ProtoBuf;

namespace BinanceTrackerDesktop.User.Data.Save.Binary;

public sealed class BinaryUserDataSaveSystem : IUserDataSaveSystem
{
    public void Write(UserData data)
    {
        if (data != null)
        {
            using (FileStream fileStream = File.Create(UserDataFile.FullPath))
            {
                Serializer.Serialize(fileStream, data);
            }
        }
    }

    public UserData Read()
    {
        if (File.Exists(UserDataFile.FullPath) == false)
        {
            return null;
        }

        using (FileStream fileStream = File.OpenRead(UserDataFile.FullPath))
        {
            return Serializer.Deserialize<UserData>(fileStream);
        }
    }
}
