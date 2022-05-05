using BinanceTrackerDesktop.Core.User.Authentication.Data;
using BinanceTrackerDesktop.Core.User.Data.Save;

namespace BinanceTrackerDesktop.Core.User.Data.Builder
{
    /// <summary>
    /// Builder for <see cref="UserData"/>
    /// </summary>
    public interface IUserDataBuilder
    {
        /// <summary>
        /// Adding key to the <see cref="UserData"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IUserDataBuilder AddKey(string value);

        /// <summary>
        /// Adding secret to the <see cref="UserData"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IUserDataBuilder AddSecret(string value);

        /// <summary>
        /// Adding currency to the <see cref="UserData"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IUserDataBuilder AddCurrency(string value);

        /// <summary>
        /// Adding currency to the <see cref="UserData"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IUserDataBuilder AddBestBalance(decimal value);

        /// <summary>
        /// Adding <paramref name="authentication"/> to the <see cref="UserData"/>
        /// </summary>
        /// <param name="authentication">Adding authentication.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        /// <exception cref="System.ArgumentException"></exception>
        IUserDataBuilder AddTwoFactor(UserTwoFactorAuthenticationData authentication);

        /// <summary>
        /// Adding notifications state to the <see cref="UserData"/> based on <paramref name="value"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder AddNotificationsStateBasedOnData(bool? value);

        /// <summary>
        /// Adding balances state to the <see cref="UserData"/> based on <paramref name="value"/>
        /// </summary>
        /// <param name="value">Adding value.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder AddBalancesStateBasedOnData(bool? value);

        /// <summary>
        /// Set balances state to hiden (true) to the <see cref="UserData"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetBalancesHiden();

        /// <summary>
        /// Set balances state to not hiden (false) to the <see cref="UserData"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetBalancesIsNotHiden();

        /// <summary>
        /// Set notifications state to disabled (false) to the <see cref="UserData"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetNotificationsDisabled();

        /// <summary>
        /// Set notifications state to enabled (true) to the <see cref="UserData"/>
        /// </summary>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder SetNotificationsEnabled();

        /// <summary>
        /// Reading existing user data and caching it to the <see cref="IUserDataBuilder"/>
        /// </summary>
        /// <param name="system">User data save system.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder ReadExistingUserDataAndCacheIt(IUserDataSaveSystem system);

        /// <summary>
        /// Reading existing user data and caching them to the <see cref="IUserDataBuilder"/> <see cref="UserData"/> and <see cref="IUserDataSaveSystem"/>
        /// </summary>
        /// <param name="system">User data save system.</param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder ReadExistingUserDataAndCacheAll(IUserDataSaveSystem system);

        /// <summary>
        /// Returning the last used <see cref="IUserDataSaveSystem"/>
        /// </summary>
        /// <param name="system">Last used <see cref="IUserDataSaveSystem"/></param>
        /// <returns>Instance to the <see cref="IUserDataBuilder"/></returns>
        IUserDataBuilder GetLastUsedSaveSystem(out IUserDataSaveSystem system);

        /// <summary>
        /// Get last used <see cref="IUserDataSaveSystem"/>
        /// </summary>
        /// <returns>Instance to the last used <see cref="IUserDataSaveSystem>"/></returns>
        IUserDataSaveSystem GetLastUsedSaveSystem();

        /// <summary>
        /// Building the <see cref="IUserDataBuilder"/>
        /// </summary>
        /// <returns>Builded <see cref="UserData"/></returns>
        UserData Build();
    }
}