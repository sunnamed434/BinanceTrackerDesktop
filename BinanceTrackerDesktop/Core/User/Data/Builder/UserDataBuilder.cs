using BinanceTrackerDesktop.Core.User.Authentication.Data;

namespace BinanceTrackerDesktop.Core.User.Data.Builder
{
    public sealed class UserDataBuilder : IUserDataBuilder
    {
        private UserData userData;



        public UserDataBuilder(UserData userData)
        {
            this.userData = userData;
        }

        public UserDataBuilder() : this(new UserData())
        {
        }



        public IUserDataBuilder AddKey(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(nameof(value));

            this.userData.Key = value;
            return this;
        }

        public IUserDataBuilder AddSecret(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(nameof(value));

            this.userData.Secret = value;
            return this;
        }

        public IUserDataBuilder AddCurrency(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(nameof(value));

            this.userData.Currency = value;
            return this;
        }

        public IUserDataBuilder AddBestBalance(decimal? value)
        {
            this.userData.BestBalance = value;
            return this;
        }

        public IUserDataBuilder AddTwoFactor(UserTwoFactorAuthenticationData authentication)
        {
            if (authentication == null)
                throw new ArgumentNullException(nameof(authentication));

            this.userData.AuthenticationData = authentication;
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
            this.userData.IsBalancesHiden = true;
            return this;
        }

        public IUserDataBuilder SetBalancesIsNotHiden()
        {
            this.userData.IsBalancesHiden = false;
            return this;
        }

        public IUserDataBuilder SetNotificationsDisabled()
        {
            this.userData.IsNotificationsEnabled = false;
            return this;
        }

        public IUserDataBuilder SetNotificationsEnabled()
        {
            this.userData.IsNotificationsEnabled = true;
            return this;
        }

        public IUserDataBuilder SetAsUserStartedApplicationFirstTime()
        {
            this.userData.IsUserStartedApplicationFirstTime = true;
            return this;
        }

        public IUserDataBuilder SetAsUserStartedApplicationNotFirstTime()
        {
            this.userData.IsUserStartedApplicationFirstTime = false;
            return this;
        }

        public IUserDataBuilder SetUserThemeAsSystem()
        {
            this.userData.Theme = Themes.Theme.System;
            return this;
        }

        public IUserDataBuilder SetUserThemeAsLight()
        {
            this.userData.Theme = Themes.Theme.Light;
            return this;
        }

        public IUserDataBuilder SetUserThemeAsDark()
        {
            this.userData.Theme = Themes.Theme.Dark;
            return this;
        }

        public UserData Build()
        {
            return this.userData;
        }
    }
}
