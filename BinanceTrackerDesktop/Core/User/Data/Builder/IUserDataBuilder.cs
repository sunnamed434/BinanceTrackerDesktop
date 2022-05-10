using BinanceTrackerDesktop.Core.User.Authentication.Data;

namespace BinanceTrackerDesktop.Core.User.Data.Builder
{
    /// <summary>
    /// Builder for <see cref="UserData"/>
    /// </summary>
    public interface IUserDataBuilder
    {
        /// <summary>
        /// Adding <see cref="UserData.Key"/> to the <see cref="UserData"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IUserDataBuilder AddKey(string value);

        /// <summary>
        /// Adding <see cref="UserData.Secret"/> to the <see cref="UserData"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IUserDataBuilder AddSecret(string value);

        /// <summary>
        /// Adding <see cref="UserData.Currency"/> to the <see cref="UserData"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IUserDataBuilder AddCurrency(string value);

        /// <summary>
        /// Adding <see cref="UserData.BestBalance"/> to the <see cref="UserData"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder AddBestBalance(decimal? value);

        /// <summary>
        /// Adding <paramref name="authentication"/> to the <see cref="UserData"/>
        /// </summary>
        /// <param name="authentication">Adding authentication.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IUserDataBuilder AddTwoFactor(UserTwoFactorAuthenticationData authentication);

        /// <summary>
        /// Adding <see cref="UserData.IsNotificationsEnabled"/> to the <see cref="UserData"/> based on <paramref name="value"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder AddNotificationsStateBasedOnData(bool? value);

        /// <summary>
        /// Adding <see cref="UserData.IsBalancesHiden"/> to the <see cref="UserData"/> based on <paramref name="value"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder AddBalancesStateBasedOnData(bool? value);

        /// <summary>
        /// Set <see cref="UserData.IsBalancesHiden"/> <see langword="true"/> to the <see cref="UserData"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetBalancesHiden();

        /// <summary>
        /// Set <see cref="UserData.IsBalancesHiden"/> <see langword="false"/> to the <see cref="UserData"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetBalancesIsNotHiden();

        /// <summary>
        /// Set <see cref="UserData.IsNotificationsEnabled"/> state to <see langword="false"/> to the <see cref="UserData"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetNotificationsDisabled();

        /// <summary>
        /// Set <see cref="UserData.IsNotificationsEnabled"/> to <see langword="true"/> to the <see cref="UserData"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetNotificationsEnabled();

        /// <summary>
        /// Set <see cref="UserData.IsUserStartedApplicationFirstTime"/> to <see langword="true"/> to the <see cref="UserData"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetAsUserStartedApplicationFirstTime();

        /// <summary>
        /// Set <see cref="UserData.IsUserStartedApplicationFirstTime"/> to <see langword="false"/> to the <see cref="UserData"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetAsUserStartedApplicationNotFirstTime();

        /// <summary>
        /// Set <see cref="UserData.Theme"/> to <see cref="Themes.Theme.System"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetUserThemeAsSystem();

        /// <summary>
        /// Set <see cref="UserData.Theme"/> to <see cref="Themes.Theme.Light"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetUserThemeAsLight();

        /// <summary>
        /// Set <see cref="UserData.Theme"/> to <see cref="Themes.Theme.Dark"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetUserThemeAsDark();

        /// <summary>
        /// Building the <see cref="IUserDataBuilder"/>
        /// </summary>
        /// <returns>Builded <see cref="UserData"/></returns>
        UserData Build();
    }
}