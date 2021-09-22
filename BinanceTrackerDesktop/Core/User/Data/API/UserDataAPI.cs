﻿using BinanceTrackerDesktop.Core.DirectoryFiles.API;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static BinanceTrackerDesktop.Core.DirectoryFiles.API.DirectoryDataControl;

namespace BinanceTrackerDesktop.Core.User.Data.API
{
    [Serializable]
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

        public UserData(string key, string secret, decimal bestBalance, bool balancesHiden, bool notificationsEnabled) : this(key, secret)
        {
            BestBalance = bestBalance;
            BalancesHiden = balancesHiden;
            NotificationsEnabled = notificationsEnabled;
        }

        public UserData() : this(string.Empty, string.Empty, decimal.Zero, false, true)
        {

        }
    }

    public interface IUserDataSaveReadSystem
    {
        void Save(UserData data);

        UserData Read();
    }

    public class BinaryUserDataSaveReadSystem : IUserDataSaveReadSystem
    {
        private readonly BinaryFormatter formatter;



        public BinaryUserDataSaveReadSystem()
        {
            formatter = new BinaryFormatter();
        }



        public void Save(UserData data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            using (FileStream fileStream = File.Create(UserDataFile.FullPath))
            {
                formatter.Serialize(fileStream, data);
                fileStream.Close();
            }
        }

        public UserData Read()
        {
            if (!File.Exists(UserDataFile.FullPath))
                return null;

            using (FileStream fileStream = File.Open(UserDataFile.FullPath, FileMode.Open))
            {
                object deserializedObject = formatter.Deserialize(fileStream);
                fileStream.Close();

                return (UserData)deserializedObject;
            }
        }
    }

    public class UserDataFile
    {
        public static readonly string FullPath = Path.Combine(ApplicationDirectoryPaths.User, RegisteredData.UserFile);
    }
}
