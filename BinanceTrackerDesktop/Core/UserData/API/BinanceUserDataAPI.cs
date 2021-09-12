using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;

namespace BinanceTrackerDesktop.Core.UserData.API
{
    public interface IBinanceUserData
    {
        string Key { get; set; }

        string Secret { get; set; }
    }

    public interface IBinanceUserDataWriter
    {
        Task WriteDataAsync(IBinanceUserData data);
    }

    public interface IBinanceUserDataReader
    {
        Task<IBinanceUserData> ReadDataAsync();
    }

    public class BinanceUserDataWriter : IBinanceUserDataWriter
    {
        public async Task WriteDataAsync(IBinanceUserData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (StreamWriter writer = new StreamWriter(BinanceUserDataFile.Path, false))
            {
                await writer.WriteAsync(JsonConvert.SerializeObject(data));
                writer.Close();

                await Task.CompletedTask;
            }
        }
    }

    public class BinanceUserDataReader : IBinanceUserDataReader
    {
        public async Task<IBinanceUserData> ReadDataAsync()
        {
            if (!File.Exists(BinanceUserDataFile.Path))
                return null;

            using (StreamReader reader = new StreamReader(BinanceUserDataFile.Path))
            {
                string data = await reader.ReadToEndAsync();
                reader.Close();

                return JsonConvert.DeserializeObject<BinanceUserData>(data);
            }
        }
    }

    public class BinanceUserData : IBinanceUserData
    {
        public string Key { get; set; }

        public string Secret { get; set; }

        public decimal BestBalance { get; set; }

        public bool BalancesHiden { get; set; }

        public bool NotificationsEnabled { get; set; }



        public BinanceUserData(string key, string secret)
        {
            Key = key;
            Secret = secret;
            BestBalance = decimal.Zero;
            BalancesHiden = false;
            NotificationsEnabled = true;
        }

        public BinanceUserData()
        {
            Key = string.Empty;
            Secret = string.Empty;
            BestBalance = decimal.Zero;
            BalancesHiden = false;
            NotificationsEnabled = true;
        }
    }

    public class BinanceUserDataFile
    {
        public const string Path = "userdata.json";
    }
}
