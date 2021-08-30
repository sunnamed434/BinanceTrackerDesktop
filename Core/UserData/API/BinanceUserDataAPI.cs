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
        public Task WriteDataAsync(IBinanceUserData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (StreamWriter writer = new StreamWriter(BinanceUserDataFile.Path, false))
                return writer.WriteAsync(JsonConvert.SerializeObject(data));
        }
    }

    public class BinanceUserDataReader : IBinanceUserDataReader
    {
        public async Task<IBinanceUserData> ReadDataAsync()
        {
            if (!File.Exists(BinanceUserDataFile.Path))
                return null;

            using (StreamReader reader = new StreamReader(BinanceUserDataFile.Path))
                return JsonConvert.DeserializeObject<BinanceUserData>(await reader.ReadToEndAsync());
        }
    }

    public class BinanceUserData : IBinanceUserData
    {
        public string Key { get; set; }

        public string Secret { get; set; }

        public decimal Balance { get; set; }

        public decimal BestBalance { get; set; }

        public bool NotificationsEnabled { get; set; }



        public BinanceUserData(string key, string secret)
        {
            Key = key;
            Secret = secret;
            Balance = decimal.Zero;
            BestBalance = decimal.Zero;
            NotificationsEnabled = true;
        }

        public BinanceUserData()
        {
            Key = string.Empty;
            Secret = string.Empty;
            Balance = decimal.Zero;
            BestBalance = decimal.Zero;
            NotificationsEnabled = true;
        }
    }

    public class BinanceUserDataFile
    {
        public const string Path = "userdata.json";
    }
}
