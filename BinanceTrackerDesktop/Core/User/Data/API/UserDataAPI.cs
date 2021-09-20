using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Core.User.Data.API
{
    public interface IUserDataWriter
    {
        Task WriteDataAsync(UserData data);
    }

    public interface IUserDataReader
    {
        Task<UserData> ReadDataAsync();
    }

    public class UserDataWriter : IUserDataWriter
    {
        public async Task WriteDataAsync(UserData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (StreamWriter writer = new StreamWriter(UserDataFile.Path, false))
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(data));
                writer.Close();

                await Task.CompletedTask;
            }
        }
    }

    public class UserDataReader : IUserDataReader
    {
        public async Task<UserData> ReadDataAsync()
        {
            if (!File.Exists(UserDataFile.Path))
                return null;

            using (StreamReader reader = new StreamReader(UserDataFile.Path))
            {
                string data = await reader.ReadToEndAsync();
                reader.Close();

                return JsonConvert.DeserializeObject<UserData>(data);
            }
        }
    }

    public class UserData
    {
        public string Key;

        public string Secret;

        public decimal BestBalance;

        public bool BalancesHiden;

        public bool NotificationsEnabled;



        public UserData(string key, string secret)
        {
            Key = key;
            Secret = secret;
            BestBalance = decimal.Zero;
            BalancesHiden = false;
            NotificationsEnabled = true;
        }

        public UserData()
        {
            Key = string.Empty;
            Secret = string.Empty;
            BestBalance = decimal.Zero;
            BalancesHiden = false;
            NotificationsEnabled = true;
        }
    }

    public class UserDataFile
    {
        public const string Path = "userdata.json";
    }

    public static class UserDataExtension
    {
        public static async Task SaveUserDataAsync(this UserData source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            await new UserDataWriter().WriteDataAsync(source);
        }
    }
}
