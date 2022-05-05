using BinanceTrackerDesktop.Core.User.Authentication.Data;
using BinanceTrackerDesktop.Core.User.Data.Save;

namespace BinanceTrackerDesktop.Core.User.Data.Builder
{
    public sealed class UserDataBuilder : IUserDataBuilder
    {
        private UserData userData;

        private IUserDataSaveSystem saveSystem; 



        public UserDataBuilder()
        {
            userData = new UserData();
        }



        public IUserDataBuilder AddKey(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(nameof(value));

            userData.Key = value;
            return this;
        }

        public IUserDataBuilder AddSecret(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(nameof(value));

            userData.Secret = value;
            return this;
        }

        public IUserDataBuilder AddCurrency(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(nameof(value));

            userData.Currency = value;
            return this;
        }

        public IUserDataBuilder AddBestBalance(decimal value)
        {
            userData.BestBalance = value;
            return this;
        }

        public IUserDataBuilder AddTwoFactor(UserTwoFactorAuthenticationData authentication)
        {
            if (authentication == null)
                throw new ArgumentNullException(nameof(authentication));

            userData.AuthenticationData = authentication;
            return this;
        }

        public IUserDataBuilder AddBalancesStateBasedOnData(bool? value)
        {
            if (value.HasValue && value.Value)
                SetBalancesHiden();
            else
                SetBalancesIsNotHiden();

            return this;
        }

        public IUserDataBuilder AddNotificationsStateBasedOnData(bool? value)
        {
            if (value.HasValue && value.Value)
                SetNotificationsDisabled();
            else
                SetNotificationsEnabled();

            return this;
        }

        public IUserDataBuilder SetBalancesHiden()
        {
            userData.IsBalancesHiden = true;
            return this;
        }

        public IUserDataBuilder SetBalancesIsNotHiden()
        {
            userData.IsBalancesHiden = false;
            return this;
        }

        public IUserDataBuilder SetNotificationsDisabled()
        {
            userData.IsNotificationsEnabled = false;
            return this;
        }

        public IUserDataBuilder SetNotificationsEnabled()
        {
            userData.IsNotificationsEnabled = true;
            return this;
        }

        public IUserDataBuilder ReadExistingUserDataAndCacheIt(IUserDataSaveSystem system)
        {
            if (system == null)
                throw new ArgumentNullException(nameof(system));

            userData = saveSystem.Read();
            return this;
        }

        public IUserDataBuilder ReadExistingUserDataAndCacheAll(IUserDataSaveSystem system)
        {
            if (system == null)
                throw new ArgumentNullException(nameof(system));

            saveSystem = system;
            userData = saveSystem.Read();
            return this;
        }

        public IUserDataBuilder GetLastUsedSaveSystem(out IUserDataSaveSystem system)
        {
            if (saveSystem == null)
                throw new InvalidOperationException();

            system = saveSystem;
            return this;
        }

        public IUserDataSaveSystem GetLastUsedSaveSystem()
        {
            GetLastUsedSaveSystem(out IUserDataSaveSystem system);

            return system;
        }

        public UserData Build()
        {
            return userData;
        }
    }
}
