using BinanceTrackerDesktop.Localizations.Language;
using BinanceTrackerDesktop.Themes;
using BinanceTrackerDesktop.User.Authentication.Data;
using BinanceTrackerDesktop.User.Data.Save;

namespace BinanceTrackerDesktop.User.Data.Builder;

public sealed class UserDataBuilder : IUserDataBuilder
{
    private UserData userData;



    public UserDataBuilder(UserData userData)
    {
        if (userData == null)
        {
            throw new ArgumentNullException(nameof(userData));
        }

        this.userData = userData;
    }

    public UserDataBuilder(IUserDataSaveSystem userDataSaveSystem)
    {
        if (userDataSaveSystem == null)
        {
            throw new ArgumentNullException(nameof(userDataSaveSystem));
        }

        this.userData = userDataSaveSystem.Read();
    }

    public UserDataBuilder() : this(new UserData())
    {
    }



    public IUserDataBuilder AddKey(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(nameof(value));
        }

        userData.Key = value;
        return this;
    }

    public IUserDataBuilder AddSecret(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(nameof(value));
        }

        userData.Secret = value;
        return this;
    }

    public IUserDataBuilder AddCurrency(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(nameof(value));
        }

        userData.Currency = value;
        return this;
    }

    public IUserDataBuilder AddBestBalance(decimal? value)
    {
        userData.BestBalance = value;
        return this;
    }

    public IUserDataBuilder AddTwoFactor(UserTwoFactorAuthenticationData authentication)
    {
        if (authentication == null)
        {
            throw new ArgumentNullException(nameof(authentication));
        }

        userData.AuthenticationData = authentication;
        return this;
    }

    public IUserDataBuilder AddBalancesStateBasedOnData(bool? value)
    {
        if (value.HasValue && value.Value)
        {
            SetBalancesHiden();
        }
        else
        {
            SetBalancesIsNotHiden();
        }

        return this;
    }

    public IUserDataBuilder AddNotificationsStateBasedOnData(bool? value)
    {
        if (value.HasValue && value.Value)
        {
            SetNotificationsEnabled();
        }
        else
        {
            SetNotificationsDisabled();
        }

        return this;
    }

    public IUserDataBuilder AddUserTheme(Theme theme)
    {
        userData.Theme = theme;
        return this;
    }

    public IUserDataBuilder AddUserLanguage(Languages language)
    {
        this.userData.Language = language;
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

    public IUserDataBuilder SetAsUserStartedApplicationFirstTime()
    {
        userData.IsUserStartedApplicationFirstTime = true;
        return this;
    }

    public IUserDataBuilder SetAsUserStartedApplicationNotFirstTime()
    {
        userData.IsUserStartedApplicationFirstTime = false;
        return this;
    }

    public IUserDataBuilder SetUserThemeAsSystem()
    {
        return AddUserTheme(Theme.System);
    }

    public IUserDataBuilder SetUserThemeAsColorBlind()
    {
        return AddUserTheme(Theme.ColorBlind);
    }

    public IUserDataBuilder SetUserThemeAsLight()
    {
        return AddUserTheme(Theme.Light);
    }

    public IUserDataBuilder SetUserThemeAsDark()
    {
        return AddUserTheme(Theme.Dark);
    }

    public UserData Build()
    {
        return userData;
    }
}
