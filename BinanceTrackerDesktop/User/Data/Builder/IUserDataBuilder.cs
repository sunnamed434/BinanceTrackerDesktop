using BinanceTrackerDesktop.Themes;
using BinanceTrackerDesktop.User.Authentication.Data;

namespace BinanceTrackerDesktop.User.Data.Builder;

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
    /// <exception cref="ArgumentException"></exception>
    IUserDataBuilder AddKey(string value);

    /// <summary>
    /// Adding <see cref="UserData.Secret"/> to the <see cref="UserData"/>
    /// </summary>
    /// <param name="value">Adding value.</param>
    /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
    /// <exception cref="ArgumentException"></exception>
    IUserDataBuilder AddSecret(string value);

    /// <summary>
    /// Adding <see cref="UserData.Currency"/> to the <see cref="UserData"/>
    /// </summary>
    /// <param name="value">Adding value.</param>
    /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
    /// <exception cref="ArgumentException"></exception>
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
    /// <exception cref="ArgumentException"></exception>
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
    /// Adding <see cref="UserData.Theme"/> to the given <paramref name="theme"/>
    /// </summary>
    /// <param name="theme">Adding Theme.</param>
    /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
    /// <exception cref="ArgumentNullException"></exception>
    IUserDataBuilder AddUserTheme(Theme theme);

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
    /// Set User Theme to <see cref="Theme.System"/> by calling method <see cref="AddUserTheme(Theme)"/>
    /// </summary>
    /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
    IUserDataBuilder SetUserThemeAsSystem();

    /// <summary>
    /// Set User Theme to <see cref="Theme.ColorBlind"/> by calling method <see cref="AddUserTheme(Theme)"/>
    /// </summary>
    /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
    IUserDataBuilder SetUserThemeAsColorBlind();

    /// <summary>
    /// Set User Theme to <see cref="Theme.Light"/> by calling method <see cref="AddUserTheme(Theme)"/>
    /// </summary>
    /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
    IUserDataBuilder SetUserThemeAsLight();

    /// <summary>
    /// Set User Theme to <see cref="Theme.Dark"/> by calling method <see cref="AddUserTheme(Theme)"/>
    /// </summary>
    /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
    IUserDataBuilder SetUserThemeAsDark();

    /// <summary>
    /// Building the <see cref="IUserDataBuilder"/>
    /// </summary>
    /// <returns>Builded <see cref="UserData"/></returns>
    UserData Build();
}